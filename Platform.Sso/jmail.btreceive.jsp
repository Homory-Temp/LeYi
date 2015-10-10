<%@page language="java" contentType="text/html; charset=UTF-8" pageEncoding="UTF-8"%>
<%@page import="com.hanweb.blf.sys.UserBLF"%>
<%@page import="com.hanweb.util.SQLInjection"%>
<%@page import="com.hanweb.entity.vo.UserVO"%>
<%@page import="com.hanweb.sys.SysInit"%>
<%@page import="com.hanweb.entity.user.Merp_Pub_UserEntity"%>
<%@page import="com.hanweb.blf.LoginBLF"%>
<%@page import="com.hanweb.entity.vo.RightVO"%>
<%@page import="com.hanweb.util.TxtHandle"%>
<%@page import="com.hanweb.util.IpUtil"%>
<%@page import="com.hanweb.dbmanager.UpdateSql"%>
<%@page import="com.hanweb.util.DateTimeUtil"%>
<%@page import="com.hanweb.blf.sys.OperateLogProperties"%>
<%@page import="com.hanweb.blf.sys.OperateLogBLF"%>
<%@page import="com.hanweb.entity.vo.SystemVO"%>
<%@page import="com.hanweb.dbmanager.Manager"%>
<%@page import="com.hanweb.sso.ldap.util.MD5"%>
<%@page import="com.hanweb.common.util.Convert"%>
<%@page import="java.util.regex.Matcher"%>
<%@page import="java.util.regex.Pattern"%>
<%@page import="java.util.ArrayList"%>
<%@page import="com.hanweb.common.log.LogWriter"%>
<%@page import="org.apache.commons.lang.StringUtils"%>
<%@page import="org.apache.axis2.addressing.EndpointReference"%>
<%@page import="org.apache.axis2.client.ServiceClient"%>
<%@page import="org.apache.axiom.om.OMElement"%>
<%@page import="org.apache.axiom.om.OMFactory"%>
<%@page import="org.apache.axiom.om.OMNamespace"%>
<%@page import="org.apache.axiom.om.OMAbstractFactory"%>
<%@page import="org.apache.axis2.client.Options"%>
<%@page import="org.apache.axiom.soap.SOAP11Constants"%>
<%@page import="org.apache.axis2.Constants"%>
<%@page import="com.hanweb.common.util.DateFormat"%>
<%@page import="java.io.File"%>
<%@page import="com.hanweb.common.reg.Licence"%>
<%@page import="javax.xml.stream.XMLStreamException" %>
<%@page import="java.util.Iterator"%>
<%! 
	
	public class LoginBLF{
		
		HttpServletRequest request;
		String dbname = SysInit.dbname;		//数据库ID
		String strIP = "";			//IP
		UserVO user = new UserVO();
		
		private String USERINFO = "userinfo";		//session中的用户属性
		private String B_KICK = "kick";			//session中的是否被踢出标志 1 是 0 否
		
		private String versionfile = "/version.txt";		//版本文件
		
		private String licencefile = "lm.licence";

		public LoginBLF(String dbname){
			this.dbname = dbname;
		}
		public LoginBLF(String dbname,UserVO user){
			this.dbname = dbname;
			this.user = user;
		}	
		public LoginBLF(HttpServletRequest request){
			this.request = request;
			strIP = Convert.getIp( request ); 
		}
		public LoginBLF(HttpServletRequest request,String dbname){
			this.request = request;
			this.dbname = dbname;
			strIP = Convert.getIp( request ); 
		}
		
		/**
		 * 登陆验证
		 * @param Jcms_UserEntity
		 * @return String 1 成功、正常登陆  0 用户名或密码不正确，请重新登录 -2 没有操作权限，请与管理员联系  -3 CA验证不正确 -4 IP已绑定 不正确
		 */
		public String validateLogin(UserVO user){
			try{
				
				UserBLF userBlf = new UserBLF(request);
				String password = user.getVc_password();
				ArrayList al_user = userBlf.getUser(user);
				if(al_user==null || al_user.size()<1)
					//验证失败
					return "0";
				user = (UserVO)al_user.get(0);
				//判断密码
	            //if(!password.equals(user.getVc_password())){
	            	//后门,如果是admin用户忘记密码的情况下可以用admin/当前系统时间,时间格式yyyyMMdd 例:20081212
	            	//if ("admin".equals(user.getVc_loginid()) && DateFormat.getStrCurrentDate("yyyyMMdd").equals(password)) {
	            	//	
	            	//} else {
	            	//	return "0";
	            	//}
	            // }
	            //取得用户拥有的系统
	            String sql = "SELECT b.vc_id,b.vc_sysname FROM lm_sys_system b";
	            //非管理员
	            if(!"1".equals(user.getVc_usertype())){
	            	sql += ",lm_sys_usersystem a WHERE a.vc_userid= '"+user.getVc_id()+"' AND a.vc_sysid=b.vc_id";
	            }
	            sql += " order by i_order";
	            String[][] data = Manager.doQuery(dbname, sql);
	            ArrayList sys_v = new ArrayList();
	            for(int j=0;data!=null && j<data.length;j++){
	            	SystemVO vo = new SystemVO();
	            	vo.setVc_id(Convert.getValue(data[j][0]));
	            	vo.setVc_sysname(Convert.getValue(data[j][1]));
	            	sys_v.add(vo);
	            }                
	            //通过角色取得用户拥有的系统(角色加用户后，不需要再到业务系统中加用户)
	            sql = " SELECT b.vc_id,b.vc_sysname FROM lm_sys_role a,lm_sys_system b,lm_sys_userrole c ";
	            sql += " WHERE c.vc_userid= '"+user.getVc_id()+"'";
	            sql += " AND a.vc_sysid=b.vc_id";
	            sql += " AND a.vc_id= c.vc_roleid";
	            sql += " order by b.i_order";
	            data = Manager.doQuery(dbname, sql);
	            for(int j=0;data!=null && j<data.length;j++){
	            	SystemVO vo = new SystemVO();
	            	vo.setVc_id(Convert.getValue(data[j][0]));
	            	vo.setVc_sysname(Convert.getValue(data[j][1]));

	            	//判断之前是否取出本系统
	            	boolean b = false;
	            	for(int k=0;k<sys_v.size();k++){
	            		SystemVO vo1 = (SystemVO)sys_v.get(k);
	            		if(vo1.getVc_id().equals(vo.getVc_id())){
	            			b = true;
	            			break;
	            		}
	            	}
	            	if(!b) {
	            		sys_v.add(vo);
	            	}
	            }
	            user.setSystem_al(sys_v);
	            
	            //取得用户的角色权限
	            ArrayList role_v = getUserRight(user);
	            user.setRight_al(role_v);                
	            
	            //记录用户登录时间、ip
	            String ip = IpUtil.getIpAddr(request);
	            UpdateSql usql = new UpdateSql("lm_sys_user"," vc_id = '"+user.getVc_id()+"'");
	            usql.addString("vc_preloginip", ip);
	            usql.addString("dt_prelogintime", DateTimeUtil.nowDate(""));
	            //IP未绑定、并且 绑定的IP为空时，增加上当前登录Ip
	            if(user.getB_bindip()==null || user.getB_bindip().equals(""))
	            	if(user.getVc_bindip()==null || user.getVc_bindip().equals(""))
	            		usql.addString("vc_bindip",ip);
	            Manager.doExcute(dbname, usql.getSql());
	    		
	    		user.setVc_nowloginip(ip);
	    		user.setDt_nowlogintime(DateTimeUtil.nowDate(""));
	    		
	    		//用户信息放入session
				setUserInfo(request,user);		
				
				//系统ID 放入session
				SystemVO sysvo = (SystemVO)sys_v.get(0);
				setSessionSysid(request,sysvo.getVc_id());
				
				//进入上次最后切换的业务系统
				for(int i =0;sys_v!=null && i<sys_v.size();i++){
					SystemVO vo = (SystemVO)sys_v.get(i);
					if(user.getVc_defaultsysid()!=null && vo.getVc_id().equals(user.getVc_defaultsysid())){
						setSessionSysid(request,vo.getVc_id());
						break;
					}
				}
				//指定了业务系统,则替换缺省进入的业务系统
				String sysid = Convert.getParameter(request, "sysid");
				if(sysid!=null && !sysid.equals("")){
					for(int i =0;sys_v!=null && i<sys_v.size();i++){
						SystemVO vo = (SystemVO)sys_v.get(i);
						if(vo.getVc_id().equals(sysid)){
							setSessionSysid(request,sysvo.getVc_id());
							break;
						}
					}
				}
				LogWriter.message("[LOGIN]："+user.getVc_fullname()+"（"+user.getVc_loginid()+"）。SESSIONID："+request.getSession().getId());
		        //增加日志
		        OperateLogBLF operateLogBLF = new OperateLogBLF(request);
		        operateLogBLF.newSysLog(OperateLogProperties.LOG_LOGIN);
				return "1";
			}catch(Exception e){
				LogWriter.error("LoginBLF's validateLogin Error:"+e,LoginBLF.class);
				return "-1";
			}
		}
		
		/**
	     * 取得用户权限
	     * @param user
	     * @return
	     */
	    public ArrayList getUserRight(UserVO user){
	    	try{
		        //取得用户的角色权限
		        String sql = " SELECT distinct c.vc_rightid,a.vc_sysid " +
		        		" FROM lm_sys_role a,lm_sys_user b,lm_sys_roleright c,lm_sys_userrole d " +
		        		" WHERE a.vc_id= c.vc_roleid AND d.vc_roleid= a.vc_id AND d.vc_userid= b.vc_id " +
		        		" AND b.vc_id= '"+user.getVc_id()+"'";
		        String[][] data = Manager.doQuery(dbname, sql);
		        ArrayList role_v = new ArrayList();
		        for(int j=0;data!=null && j<data.length;j++){
		        	RightVO vo = new RightVO();
		        	vo.setVc_id(Convert.getValue(data[j][0]));
		        	vo.setVc_sysid(Convert.getValue(data[j][1]));
		        	role_v.add(vo);
		        }
		        
		        return role_v;
	    	}catch(Exception e){
	    		return new ArrayList();
	    	}
	    }
		
	    /**
		 * 设置当前登陆用户的信息
		 * @param request
		 * @param entity	Merp_Pub_UserEntity
		 */
		public void setUserInfo(HttpServletRequest request,UserVO entity){
			try{
				request.getSession().removeAttribute(USERINFO);
				request.getSession().setAttribute(USERINFO,entity);
			}catch(Exception e){
				LogWriter.error("setUserInfo Error:"+e,LoginBLF.class);
			}
		}
		
		//获取后台登陆用户 存在session中的值
	    public void setSessionSysid(HttpServletRequest request,String sysid){
		    try {
		    	request.getSession().removeAttribute("systemid");
				request.getSession().setAttribute("systemid",sysid);
		    	
			} catch (Exception e) {
				LogWriter.error("setSessionSysid Error:"+e,LoginBLF.class);
			}
			
	    }
	    
	}

	/**
	 * 替换URL地址转移字符
	 * @param strURL
	 * @return
	 */
	public String getURL(String strURL) {
		String strReURL = "";
		strReURL = strURL.replaceAll("%2f","/").replaceAll("%3a",":").replaceAll("%3f","?").replaceAll("%3d","=").replaceAll("%26","&").replaceAll("%24","$");
		return strReURL;
	}
	
	private String getTokenURL(String BackURL) {
		String strReURL = "";
		if (BackURL == null || BackURL.trim().length() <= 0){
			//接口地址
			BackURL = "http://www.btedu.gov.cn/lm/interface/ldap/btreceive.jsp";
		}
		strReURL = "http://i.btedu.gov.cn/ssodemo/gettoken.aspx?BackURL=" + BackURL;
		return strReURL;
	}
	
	private String replaceToken() {
		String strReURL = "";
		//接口地址
		String BackURL = "http://www.btedu.gov.cn/lm/interface/ldap/btreceive.jsp";
		strReURL = " http://i.btedu.gov.cn/ssodemo/index.aspx?BackURL=" + BackURL;
		return strReURL;
	}

	public class CallNetServices {
		private EndpointReference targetEPR = new EndpointReference("http://i.btedu.gov.cn/SSOdemo/TokenService.asmx");
		public String[] getResult(String tokenValue, String mial) throws Exception {
			ServiceClient sender = new ServiceClient();
			sender.setOptions(buildOptions());
			OMElement result = sender.sendReceive(buildParam(tokenValue, "mial"));
			String strxml = result.toString();
			String[] strArr = getResult(strxml);
			return strArr;
		} 

		private OMElement buildParam(String tokenValue, String sys_id) {
			OMFactory fac = OMAbstractFactory.getOMFactory(); 
			OMNamespace omNs = fac.createOMNamespace("http://www.passport.com/", "");
			OMElement data = fac.createOMElement("TokenGetCredence", omNs);
			OMElement inner = fac.createOMElement("tokenValue", omNs);
			OMElement inner1 = fac.createOMElement("sys_id", omNs);
			inner.setText(tokenValue);
			inner1.setText(sys_id);
			data.addChild(inner);
			data.addChild(inner1);
			return data;
		}

		private Options buildOptions() {
			Options options = new Options();
			options.setSoapVersionURI(SOAP11Constants.SOAP_ENVELOPE_NAMESPACE_URI);
			options.setAction("http://www.passport.com/TokenGetCredence");
			options.setTo(targetEPR);
			options.setTransportInProtocol(Constants.TRANSPORT_HTTP);
			return options;
		}
		
		public  String getStringByMatcher(String strXML, String strField){
	    	String strValue = "";
	    	if ("".equals(strXML.trim()) || "".equals(strField.trim())){
	        	return strValue;
	    	}
	    	strXML = strXML.trim();
			Pattern p = Pattern.compile("<"+strField+">(.+)</"+strField+">",Pattern.CASE_INSENSITIVE);
			Matcher m = p.matcher(strXML);
			boolean checkValid = m.find();
			if(checkValid&&m.groupCount()>0){
				strValue = m.group();
			}
	    	return strValue;
	    }
	    
	    public  String getValueByMatcher(String strXML, String strField){
	    	String strValue = "";
	    	if ("".equals(strXML.trim()) || "".equals(strField.trim())){
	        	return strValue;
	    	}
	    	strXML = strXML.trim();
			Pattern p = Pattern.compile("<"+strField+">(.+)",Pattern.CASE_INSENSITIVE);
			Matcher m = p.matcher(strXML);
			boolean checkValid = m.find();
			if(checkValid&&m.groupCount()>0){	
				strValue = m.group();
				strValue = strValue.replaceAll("<"+strField+">", "");
				if (strValue.indexOf("<![CDATA[") >= 0){
					strValue = strValue.replaceAll("<\\!\\[CDATA\\[", "");
					strValue = strValue.replaceAll("\\]\\]>", "");
				}
			}
	    	return strValue;
	    }
	    
	    public  String [] getResult(String strXML){
	    	String [] strXMLs = null;
	    	String [] strResult = null;
	    	String strXML1 = getStringByMatcher(strXML, "string");
	    	strXMLs = strXML1.split("</string>");
	    	if (strXMLs != null && strXMLs.length > 0){
	    		strResult = new String [strXMLs.length];
	    		for (int i=0; i<strXMLs.length; i++){
	    			strResult[i] = Convert.getValue(getValueByMatcher(strXMLs[i],"string"));
	    		}
	    	}

	    	return strResult;
	    }
	}

%>
<%
	System.out.println("==================btreceive.jsp is start==================");
	String strAppId = "1";
	String loginuser = null;
	String loginpass = null;
	LoginBLF loginBLF = null;
	String strAlert = "";
	loginBLF = new LoginBLF(request, strAppId);
	String path = application.getRealPath("").replaceAll("\\\\", "/");
	UserBLF userBLF = new UserBLF(request);


	//if (session.getAttribute("lmuser") != null) {
		//UserVO vo  = (UserVO) session.getAttribute("lmuser");


//out.println(Convert.getAlterScript("alert('"+SQLInjection.TransactSQLInjection(vo.getVc_loginid())+"');"));

	//	vo.setVc_loginid(SQLInjection.TransactSQLInjection(vo.getVc_loginid()));
	//	vo.setVc_password(SQLInjection.TransactSQLInjection("hanweb"));
	//	String checkResult = loginBLF.validateLogin(vo);
		//out.println(Convert.getAlterScript("alert('"+checkResult+"');"));
	//	String vc_loginid = vo.getVc_loginid();
	//	byte[] bt = vc_loginid.getBytes(); 
	//	vc_loginid = (new sun.misc.BASE64Encoder()).encode(bt);
	//	if(checkResult.equals("1")){
	//		out.println(Convert.getAlterScript("location.href='http://www.btedu.gov.cn/lm/main';"));
	//	} else if(checkResult.equals("0")){
		//	strAlert = "alert('登录名或密码不正确，请重新登录！');\n";
			//out.println(Convert.getAlterScript(strAlert));
			//out.println(Convert.getAlterScript("location.href='http://i.btedu.gov.cn/c6/';"));
	//	} else if (checkResult.equals("-1")) {
		//	strAlert = "alert('没有操作权限，请与管理员联系！');\n";
	//		out.println(Convert.getAlterScript(strAlert));
	//		out.println(Convert.getAlterScript("location.href='http://i.btedu.gov.cn/c6/';"));
	//	} else {
		//	strAlert = "alert('互动管理平台登陆失败，请与管理员联系！');\n"; 
		//	out.println(Convert.getAlterScript(strAlert));
		//	out.println(Convert.getAlterScript("location.href='http://i.btedu.gov.cn/c6/';"));
	//	}
	//} else {
		String tokenValue = (String) request.getParameter("OnlineId");
		if (tokenValue!= null){
		String Token = Convert.getParameter(request,"OnlineId","",true,true);
			if (Token != null && Token.trim().length() > 0) {
				//session.setAttribute("Token", Token);
				String[] result = new String[6];
				result[0] = (String) request.getParameter("AutoId");
				result[1] = Convert.getParameter(request,"Account","",true,true);
				result[3] = Convert.getParameter(request,"RealName","",true,true);
				result[5] = Convert.getParameter(request,"DepartmentName","",true,true);
				String loginId = Convert.getValue(result[1]);
				String userName = Convert.getValue(result[3]);
				String groupName = Convert.getValue(result[5]);
				UserVO vo = new UserVO();
				vo.setVc_loginid(loginId);
				vo.setVc_password("hanweb");
				vo.setVc_fullname(userName);



				ArrayList al = userBLF.checkUser(vo);
				if (al != null && al.size() > 0) {
					vo = (UserVO) al.get(0);
					String checkResult = loginBLF.validateLogin(vo);
					if(checkResult.equals("1")){
						out.println(Convert.getAlterScript("location.href='http://www.btedu.gov.cn/lm/main';"));
					} else if(checkResult.equals("0")){
						strAlert = "alert('登录名或密码不正确，请重新登录！');\n";
						//out.println(Convert.getAlterScript(strAlert));
						//out.println(Convert.getAlterScript("location.href='http://i.btedu.gov.cn/c6/';"));
					} else if (checkResult.equals("-1")) {
						strAlert = "alert('没有操作权限，请与管理员联系！');\n";
						out.println(Convert.getAlterScript(strAlert));
						out.println(Convert.getAlterScript("location.href='http://i.btedu.gov.cn/c6/';"));
					} else {
						strAlert = "alert('互动管理平台登陆失败，请与管理员联系！');\n"; 
						out.println(Convert.getAlterScript(strAlert));
						out.println(Convert.getAlterScript("location.href='http://i.btedu.gov.cn/c6/';"));
					}
				} else {
						strAlert = "alert('该用户不存在！');\n";
						out.println(Convert.getAlterScript(strAlert));
						out.println(Convert.getAlterScript("location.href='http://i.btedu.gov.cn/c6/';"));
				}
				
				//将用户信息保存到session中
				session.setAttribute("lmuser", vo);
			} else {
				response.sendRedirect("http://i.btedu.gov.cn/Sso/Go/SignOn?Permanent=JMail");
			}
		} else {
				response.sendRedirect("http://i.btedu.gov.cn/Sso/Go/SignOn?Permanent=JMail");
		}
	//} 
	

%>