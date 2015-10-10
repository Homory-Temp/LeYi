<%@page language="java" contentType="text/html; charset=UTF-8" pageEncoding="UTF-8"%>
<%@page import="com.hanweb.sso.ldap.util.MD5"%>
<%@page import="jcms.blf.user.LdapBLF"%>
<%@page import="com.hanweb.common.util.Convert"%>
<%@page import="jcms.blf.user.UserRightBLF"%>
<%@page import="jcms.entity.Merp_Pub_UserEntity"%>
<%@page import="java.util.regex.Matcher"%>
<%@page import="java.util.regex.Pattern"%>
<%@page import="jcms.blf.user.UserBLF"%>
<%@page import="java.util.ArrayList"%>
<%@page import="jcms.blf.LoginBLF"%>
<%@page import="jcms.entity.Merp_Pub_GroupsEntity"%>
<%@page import="jcms.dbmanager.Manager"%>
<%@page import="jcms.blf.TableKeyBLF"%>
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
<%@page import="jcms.entity.Jcms_WebinfomationEntity"%>
<%@page import="jcms.entity.Jcms_WebserverEntity"%>
<%@page import="jcms.dbmanager.UpdateSql"%>
<%@page import="com.hanweb.common.util.DateFormat"%>
<%@page import="jcms.util.SysInit"%>
<%@page import="jcms.blf.user.epass.HMAC_MD5"%>
<%@page import="jcms.blf.user.RoleBLF"%>
<%@page import="jcms.blf.user.UserMenuBLF"%>
<%@page import="jcms.entity.Merp_Pub_UsermenuEntity"%>
<%@page import="java.io.File"%>
<%@page import="com.hanweb.common.reg.Licence"%>
<%@page import="javax.xml.stream.XMLStreamException" %>

<%@page import="java.util.Iterator"%><jsp:useBean id="sys" class="jcms.sys.SysInfo"></jsp:useBean>
<%!
	public ArrayList getUserEntity(Merp_Pub_UserEntity entity){
		String strAppID = "1";
		ArrayList al = new ArrayList();
		try{
			String strSql = "SELECT c_id,vc_loginid,vc_password,vc_username,vc_usergroupid," +
					"vc_headship,vc_comptel,vc_compfax,vc_hometel,vc_mobile," +
					"vc_email,vc_qq,vc_msn,c_enable,vc_ip," +
					"c_accesstime,n_loginfail,c_failtime,c_alertflag,n_alertinterval," +
					"vc_usertype,i_defaultwebid ,i_age,c_sex,vc_address, " +
					"vc_post,vc_pwdquestion,vc_pwdanswer,c_createtime,vc_firstspell,vc_userkey,"+
					"(SELECT vc_usergroupname FROM merp_pub_usergroup WHERE c_id = a.vc_usergroupid) vc_usergroupname "+
					" "+
					" FROM merp_pub_user a WHERE 1=1";
			if(entity.getC_id()!=null && !entity.getC_id().equals(""))
				strSql += " AND c_id='"+entity.getC_id()+"'";
			if(entity.getVc_loginid()!=null && !entity.getVc_loginid().equals(""))
				strSql += " AND vc_loginid='"+entity.getVc_loginid()+"'";
			//if(entity.getVc_password()!=null && !entity.getVc_password().equals(""))
			//	strSql += " AND vc_password='"+md5.encrypt(entity.getVc_password(),key)+"'";		//加密操作
			if(!"".equals(entity.getC_enable()))
				strSql += " AND c_enable='"+entity.getC_enable()+"'";
			if(entity.getVc_usertype()!=null && !"".equals(entity.getVc_usertype()))
				strSql += " AND vc_usertype='"+entity.getVc_usertype()+"'";		
			//首字母查询
			if(entity.getVc_firstspell()!=null && !"".equals(entity.getVc_firstspell()))
				strSql += " AND vc_firstspell='"+entity.getVc_firstspell()+"'";
			
			if(entity.getVc_username()!=null && !"".equals(entity.getVc_username()))
				strSql += " AND vc_username='"+ entity.getVc_username().replaceAll("'", "''")+"'";
			
			if(entity.getIds()!=null && !entity.getIds().equals(""))
				strSql += " AND c_id in ("+entity.getIds()+")";
			if(entity.getIds_not()!=null && !entity.getIds_not().equals(""))
				strSql += " AND c_id not in ("+entity.getIds_not()+")";
			
			//if( entity.getVc_usergroupid() != null && entity.getVc_usergroupid().trim().length() > 0 )
			//	strSql += " AND vc_usergroupid='"+ entity.getVc_usergroupid()+"'";
System.out.println("----select user----"+strSql);			
			String[][] data = Manager.doQuery(strAppID, strSql);
	
			for(int i=0;data!=null && i<data.length;i++){
				Merp_Pub_UserEntity entity1 = new Merp_Pub_UserEntity();
				
				entity1.setC_id 				(Convert.getValue(data[i][0]));    
				entity1.setVc_loginid           (Convert.getValue(data[i][1]));
				entity1.setVc_password          (Convert.getValue(data[i][2]));
				
				if(entity1.getVc_password()!=null)
					entity1.setVc_password (md5decode(entity1.getVc_password()));		//解密操作	
				entity1.setVc_password_md5(Convert.getValue(data[i][2]));				//存放数据库中的密码(未解密的)
	
				entity1.setVc_username          (Convert.getValue(data[i][3]));
				entity1.setVc_usergroupid       (Convert.getValue(data[i][4]));
				
				entity1.setVc_headship          (Convert.getValue(data[i][5]));
				entity1.setVc_comptel           (Convert.getValue(data[i][6]));
				entity1.setVc_compfax           (Convert.getValue(data[i][7]));
				entity1.setVc_hometel           (Convert.getValue(data[i][8]));
				entity1.setVc_mobile            (Convert.getValue(data[i][9]));
				
				entity1.setVc_email             (Convert.getValue(data[i][10]));
				entity1.setVc_qq           		(Convert.getValue(data[i][11]));
				entity1.setVc_msn           	(Convert.getValue(data[i][12]));
				entity1.setC_enable      		(Convert.getValue(data[i][13]));
				entity1.setVc_ip      			(Convert.getValue(data[i][14]));
				
				entity1.setC_accesstime  		(Convert.getValue(data[i][15]));
				entity1.setN_loginfail          (Convert.getStringValueInt(data[i][16]));
				entity1.setC_failtime           (Convert.getValue(data[i][17]));
				entity1.setC_alertflag          (Convert.getValue(data[i][18]));
				entity1.setN_alertinterval      (Convert.getStringValueInt(data[i][19]));
				
				entity1.setVc_usertype          (Convert.getValue(data[i][20]));
				entity1.setI_defaultwebid       (Convert.getStringValueInt(data[i][21]));//默认登陆网站
				entity1.setI_age         		(Convert.getStringValueInt((data[i][22])));
				entity1.setC_sex           		(Convert.getValue(data[i][23]));
				entity1.setVc_address           (Convert.getValue(data[i][24]));
				
				entity1.setVc_post      		(Convert.getValue(data[i][25]));
				entity1.setVc_pwdquestion       (Convert.getValue(data[i][26]));				
				entity1.setVc_pwdanswer         (Convert.getValue(data[i][27]));
				entity1.setC_createtime         (Convert.getValue(data[i][28]));
				entity1.setVc_firstspell		(Convert.getValue(data[i][29]));
				
				entity1.setVc_userkey		(Convert.getValue(data[i][30]));
				entity1.setVc_usergroupname		(Convert.getValue(data[i][31]));	
				
				//取得用户的管理机构范围
				strSql = "SELECT vc_userid,vc_groupid," +
						"(SELECT vc_usergroupname FROM merp_pub_usergroup WHERE c_id = a.vc_groupid) " +
						" FROM merp_pub_usergrange a " +
						" WHERE vc_userid = '"+entity1.getC_id()+"'";
System.out.println("----select group----"+strSql);				
				String[][] data1 = Manager.doQuery(strAppID, strSql);
				String vc_grouprangeid = "";
				String vc_grouprangename = "";
				for(int j=0;j<data1.length;j++){
					vc_grouprangeid += ","+data1[j][1];
					vc_grouprangename += ","+data1[j][2];
				}
				if(vc_grouprangeid.length()>0)
					vc_grouprangeid = vc_grouprangeid.substring(1);
				if(vc_grouprangename.length()>0)
					vc_grouprangename = vc_grouprangename.substring(1);
		
				entity1.setVc_grouprangeid(vc_grouprangeid);
				entity1.setVc_grouprangename(vc_grouprangename);
				al.add(entity1);
			}
			
			return al;
		}catch(Exception e){
			LogWriter.error("getUserEntity Error:"+e,UserBLF.class);
			return new ArrayList();
		}
	}

	public class LoginBLF {
		HttpServletRequest request;
		String strAppID = "1";		//网站群ID
		int nWebID = 1;				//网站ID
		String strIP = "";			//IP
		private  String key = "jcms2008";
		Merp_Pub_UserEntity userEn = new Merp_Pub_UserEntity();
		
		public  String USERINFO = "userinfo";		//session中的用户属性
		public  String WEBUSERINFO = "webuserinfo";		//session中的外网用户属性
		private  String WEBINFO = "webinfo";			//session中的用户网站属性
		public  String B_KICK = "kick";			//session中的是否被踢出标志 1 是 0 否
		
		public  String checkLicence = "系统未注册，不能继续使用，请联系管理员";
		 
		public LoginBLF(String strAppID){
			this.strAppID = strAppID;
		}
		public LoginBLF(String strAppID,Merp_Pub_UserEntity userEn){
			this.strAppID = strAppID;
			this.userEn = userEn;
		}	
		public LoginBLF(HttpServletRequest request){
			this.request = request;
			strIP = Convert.getIp(request).split(",")[0]; 
		}
		public LoginBLF(HttpServletRequest request,String strAppID){
			this.request = request;
			this.strAppID = strAppID;
			strIP = Convert.getIp(request).split(",")[0]; 
		}	
		/**
		 * 取得当前登陆用户的信息
		 * @param request
		 * @return
		 */
		public  Merp_Pub_UserEntity getUserInfo(HttpServletRequest request){
			try{
				Merp_Pub_UserEntity entity = new Merp_Pub_UserEntity();
				if(request.getSession()!=null)
				   entity = (Merp_Pub_UserEntity)request.getSession().getAttribute(USERINFO);
				if(entity ==null)
					entity = new Merp_Pub_UserEntity();
				return entity;
			}catch(Exception e){
				LogWriter.error("getUserInfo Error:"+e,LoginBLF.class);
				return new Merp_Pub_UserEntity();
			}
		}
		/**
		 * 设置当前登陆用户的信息
		 * @param request
		 * @param entity	Merp_Pub_UserEntity
		 */
		public  void setUserInfo(HttpServletRequest request,Merp_Pub_UserEntity entity){
			try{
				request.getSession().removeAttribute(USERINFO);
				request.getSession().setAttribute(USERINFO,entity);
	
			}catch(Exception e){
				LogWriter.error("setUserInfo Error:"+e,LoginBLF.class);
			}
		}	
		/**
		 * 取得当前登陆外网用户的信息
		 * @param request
		 * @return
		 */
		public  Merp_Pub_UserEntity getWebUserInfo(HttpServletRequest request){
			try{
				Merp_Pub_UserEntity entity = new Merp_Pub_UserEntity();
				if(request.getSession()!=null)
				   entity = (Merp_Pub_UserEntity)request.getSession().getAttribute(WEBUSERINFO);
				if(entity ==null)
					entity = new Merp_Pub_UserEntity();
				return entity;
			}catch(Exception e){
				LogWriter.error("getWebUserInfo Error:"+e,LoginBLF.class);
				return new Merp_Pub_UserEntity();
			}
		}
		/**
		 * 设置当前登陆外网用户的信息
		 * @param request
		 * @param entity	Merp_Pub_UserEntity
		 */
		public  void setWebUserInfo(HttpServletRequest request,Merp_Pub_UserEntity entity){
			try{
				request.getSession().removeAttribute(WEBUSERINFO);
				request.getSession().setAttribute(WEBUSERINFO,entity);
	
			}catch(Exception e){
				LogWriter.error("setWebUserInfo Error:"+e,LoginBLF.class);
			}
		}		
		/**
		 * 取得当前用户使用的网站属性
		 * @param request
		 * @return
		 */
		public Jcms_WebinfomationEntity getWebInfo(HttpServletRequest request){
			try{
				Jcms_WebinfomationEntity entity = new Jcms_WebinfomationEntity();
				if(request.getSession()!=null)
					entity = (Jcms_WebinfomationEntity)request.getSession().getAttribute(WEBINFO);
				return entity;
			}catch(Exception e){
				LogWriter.error("getWebInfo Error:"+e,LoginBLF.class);
				return new Jcms_WebinfomationEntity();
			}
		}
		/**
		 * 设置当前用户当前使用的网站属性
		 * @param request
		 * @param entity	Jcms_WebinfomationEntity
		 */
		public void setWebInfo(HttpServletRequest request,Jcms_WebinfomationEntity entity){
			try{
				request.getSession().setAttribute(WEBINFO,entity);
			}catch(Exception e){
				LogWriter.error("setWebInfo Error:"+e,LoginBLF.class);
			}
		}	
		/**
		 * 取得网站群集合
		 * @return ArrayList Jcms_WebserverEntity集合
		 */
		public ArrayList getWebServer(){
			ArrayList al = new ArrayList();
			try{
				String strSql = "SELECT i_id,i_groupid,vc_groupname" +
						" FROM  jcms_webserver a WHERE b_deleted=0";
	
				String[][] data = Manager.doQuery("1", strSql);
				for(int i=0;data!=null && i<data.length;i++){
					Jcms_WebserverEntity entity1 = new Jcms_WebserverEntity();
					entity1.setI_id(Convert.getStringValueInt(data[i][0]));
					entity1.setI_groupid(Convert.getStringValueInt(data[i][1]));
					entity1.setVc_groupname(data[i][2]);
					al.add(entity1);
				} 
				
				return al;
			}catch(Exception e){
				LogWriter.error("getWebServer Error:"+e,LoginBLF.class);
				return new ArrayList();
			}
		}
		
		/**
		 * 验证从LDAP登陆过来的用户
		 * @param entity
		 * @return String 1 成功、正常登陆  0 用户名或密码不正确，请重新登录 -1 没有操作权限，请与管理员联系  
		 * @return
		 */
		public String validateLdapLogin(Merp_Pub_UserEntity entity){
			String strResult = "";
			if(entity == null)
				return "0"; 
			try{
				MD5 md5 = new MD5();
				//查找用户是否存在
				String sql = "SELECT c_id FROM merp_pub_user " +
						"WHERE  vc_loginid='"+entity.getVc_loginid()+"' " +
						//" AND vc_password='"+md5.encrypt(entity.getVc_password(),key)+"'" +
						"AND vc_usertype='"+entity.getVc_usertype()+"'";
				String[][] data = Manager.doQuery(strAppID, sql);
				if (data == null || data.length == 0)
					 strResult="0";
				else
					 strResult="1";
				
				return strResult;
			}catch(Exception e ){
				LogWriter.error("validateLdapLogin Error:"+e,LoginBLF.class);
				return "0";
			}
		}
		/**
		 * 前台授权阅读本地登陆验证
		 * @param Jcms_UserEntity
		 * @param pwd_encrypt  密码是否加密   1 加密验证 0 明码验证
		 * @return String 1 成功、正常登陆  0 用户名或密码不正确，请重新登录 -1 没有操作权限，请与管理员联系  
		 */
		public String validateExtraLogin(Merp_Pub_UserEntity entity,String pwd_encrypt) {
			if(pwd_encrypt==null)
				pwd_encrypt = "0";
			try{
					UserBLF userBlf = new UserBLF(strAppID);
					String password = entity.getVc_password();
					entity.setVc_password("");
					entity.setVc_usertype("");
					ArrayList al_user = userBlf.getUserEntity(entity);
					if(al_user==null || al_user.size()<1){
System.out.println("validateExtraLogin()  验证失败"+"----user.size="+al_user.size());					
						//验证失败
						return "0";
					}else{
						entity = (Merp_Pub_UserEntity)al_user.get(0);
	
						//判断密码是否正确
						if( ("0".equals(pwd_encrypt) && !password.equals(entity.getVc_password()))
							||
							("1".equals(pwd_encrypt) && !password.equals(entity.getVc_password_md5()))
							){
							//验证失败
							//更新用户表中的失败次数
							UpdateSql upsql = new UpdateSql("merp_pub_user","c_id='"+entity.getC_id()+"'");
							upsql.addField("n_loginfail", "n_loginfail+1");
							upsql.addString("c_failtime", DateFormat.getStrCurrentDate());
							Manager.doExcute(strAppID, upsql.getSql());
System.out.println("validateExtraLogin()  验证失败");								
							return "0";
						}
						
						//判断用户是否有效
						if(!"1".equals(entity.getC_enable())){
							return "用户已失效，请联系管理员！";
						}
						
						//更新用户表中的登陆时间
						UpdateSql upsql = new UpdateSql("merp_pub_user","c_id='"+entity.getC_id()+"'");
						upsql.addString("c_accesstime", DateFormat.getStrCurrentDate());
						upsql.addString("vc_ip", strIP);
						upsql.addInt("n_loginfail", 0);
						Manager.doExcute(strAppID, upsql.getSql());
					}
				return "1";
			}catch(Exception e){
				LogWriter.error("validateExtraLogin Error:"+e, LoginBLF.class);
				return "-2";
			}
		}
		
		/**
		 * 登陆验证
		 * @param Jcms_UserEntity
		 * @param pwd_encrypt  密码是否加密   1 加密验证 0 明码验证
		 * @return String 1 成功、正常登陆  0 用户名或密码不正确，请重新登录 -1 没有操作权限，请与管理员联系  
		 */
		public String validateLogin(Merp_Pub_UserEntity entity,String pwd_encrypt){
			//int daibanWebId = entity.getI_defaultwebid();//待办接口进入网站ID
			int daibanWebId = 1;
			String strSql = "";
			if(pwd_encrypt==null)
				pwd_encrypt = "0";
			try{
					//先验证注册文件
					//if(!"1".equals(checkLicence))
					//	return checkLicence;
				
					UserBLF userBlf = new UserBLF("1");
System.out.println("1111111111111111111111111111111111111");				
					String password = entity.getVc_password();
System.out.println("btreceive.jsp validateLogin() password = " + password);					
					entity.setVc_password("");
					entity.setVc_usertype("0");	//内网用户
					ArrayList al_user = getUserEntity(entity);
					
					if(al_user==null || al_user.size()<1){
System.out.println("validateLogin()  验证失败"+"----user.size="+al_user.size());				
						//验证失败
						return "0";
					}else{
						entity = (Merp_Pub_UserEntity)al_user.get(0);
						
	                    //判断用户是否失效
						if(!"1".equals(entity.getC_enable())){
System.out.println("btreceive.jsp validateLogin() user was unabled");
							return "用户已失效，请联系管理员！";
						}
	
						/**************************************************************/
						//取得用户的角色
						RoleBLF roleBlf = new RoleBLF(strAppID);
						ArrayList al_roles = roleBlf.getUserRoles(entity);
						entity.setAl_roles(al_roles);
						//如果没有选择角色，则提示??
						if(al_roles==null || al_roles.size()==0)
							return "-1";
						
						
						//取得用户的菜单功能模块权限
						ArrayList al_rights = roleBlf.getUserRights(entity);
						entity.setAl_rights(al_rights);
						 
						
						//更新用户表中的登陆时间
						UpdateSql upsql = new UpdateSql("merp_pub_user","c_id='"+entity.getC_id()+"'");
						upsql.addString("c_accesstime", DateFormat.getStrCurrentDate());
						upsql.addString("vc_ip", strIP);
						upsql.addInt("n_loginfail", 0);
						Manager.doExcute(strAppID, upsql.getSql());
						
						entity.setC_accesstime(DateFormat.getStrCurrentDate());
					}
				//}
				
				/* 设置用户拥有的网站属性,默认取最后一次登录的网站，没有则取第一个网站属性
				 * 检查用户是否系统管理员：是 则取得所有网站；否 则通过角色取得相关网站
				 * 根据用户拥有的角色进行判断(角色对应的网站)
				*/
				if(UserRightBLF.isSysAdminUser(entity))
					strSql = "SELECT a.i_id,a.vc_webname,a.vc_domain,a.vc_modalpath,a.vc_modalobjectpath," +
						"a.vc_publishpath,a.vc_infopath,a.vc_mainwebaddr,a.c_createtime,a.b_needwebimage,a.b_read " +
						" FROM jcms_webinfomation a" +
						" ORDER BY a.i_id";			
				else
					strSql = "SELECT distinct a.i_id,a.vc_webname,a.vc_domain,a.vc_modalpath,a.vc_modalobjectpath," +
						"a.vc_publishpath,a.vc_infopath,a.vc_mainwebaddr,a.c_createtime,a.b_needwebimage,a.b_read " +
						" FROM jcms_webinfomation a,merp_pub_role b,merp_pub_roledetail c" +
						" WHERE a.i_id=b.i_webid"+		//关联webid
						" AND b.c_id = c.c_roleid AND c.c_type='0'"+
						" AND c.vc_userorgroup ='"+entity.getC_id() + "' " +
						" ORDER BY a.i_id";			//关联用户ID
				String[][] data = Manager.doQuery(strAppID, strSql);
	
				
				Jcms_WebinfomationEntity webinfoEn_first = new Jcms_WebinfomationEntity();
				boolean b_setWeb = false;
							
				for(int i=0;data!=null && i<data.length;i++)
				{
					Jcms_WebinfomationEntity webinfoEn = new Jcms_WebinfomationEntity();
					webinfoEn.setI_id(Convert.getStringValueInt(data[i][0]));		//webId
					webinfoEn.setVc_webname(Convert.getValue((data[i][1])));		//webName
					webinfoEn.setVc_domain(Convert.getValue((data[i][2])));		//strDomain
					webinfoEn.setVc_modalpath(Convert.getValue((data[i][3])));		//modalPath
					webinfoEn.setVc_modalobjectpath(Convert.getValue((data[i][4])));		//modalObjectPath
					webinfoEn.setVc_publishpath(Convert.getValue((data[i][5])));		//sitePath
					webinfoEn.setVc_infopath(Convert.getValue((data[i][6])));		//infoPath
					webinfoEn.setVc_mainwebaddr(Convert.getValue((data[i][7])));		//mainWebAdd
					webinfoEn.setC_createtime(Convert.getValue((data[i][8])));		//vc_createtime
					webinfoEn.setB_needwebimage( Convert.getStringValueInt( data[i][9]));  //b_needwebimage
					webinfoEn.setB_read(Convert.getStringValueInt(data[i][10]));
					if(daibanWebId>0){//待办接口进入
						if(webinfoEn.getI_id()==daibanWebId){
							//取得的网站与用户待办接口网站一致时，设置登陆用户的网站属性
							setWebInfo(request,webinfoEn);
							b_setWeb = true;
							break;
						}
					}else if(webinfoEn.getI_id()==entity.getI_defaultwebid()){
						//取得的网站与用户最后切换的网站一致时，设置登陆用户的网站属性,
						setWebInfo(request,webinfoEn);
						b_setWeb = true;
						break;
					}
					//取第一个网站
					if(i==0)
						webinfoEn_first = webinfoEn;
				}
				//如果未取得与用户最后切换的网站一致的网站，默认设置第一个网站为登陆网站
				
				if(!b_setWeb)
					setWebInfo(request,webinfoEn_first);
	
				
				//设置用户的登陆IP
				entity.setVc_ip(strIP);
				
				//设置session中的用户信息
				entity.setAppid(strAppID);
				setUserInfo(request,entity);
				
				//取得用户的二级菜单
				UserMenuBLF menuBlf = new UserMenuBLF(request);
				Merp_Pub_UsermenuEntity menuEn = new Merp_Pub_UsermenuEntity();
				menuEn.setC_grade("2");
				menuEn.setVc_userid(entity.getC_id());
				menuEn.setC_enable("1");
				//Jcms_WebinfomationEntity web = getWebInfo(request);	//设置当前所属网站
				//menuEn.setI_webid(web.getI_id());
				ArrayList al_menu = menuBlf.getUserMenuEntity(menuEn);
				entity.setAl_menus(al_menu);
				
				setUserInfo(request,entity);		//再放一遍
	
				return "1";
			}catch(Exception e){
				LogWriter.error("validateLogin Error:"+e,LoginBLF.class);
				return "-2";
			}
		}
		/**
		 * 检查注册文件
		 * @param HttpServletRequest reques
		 * @return String
		 */
		public  void checkLincese(ServletContext context) {
			try{
				String filename = context.getRealPath("/jcms.licence");
				//1、检查是否存在注册文件
				File file = new File(filename);
				if(!file.exists()){
					checkLicence = "系统未注册，不能继续使用，请联系管理员";
					System.out.println(checkLicence);
					return ;
				}
				
				//2、检查是否与MAC地址相符
				Licence l = new Licence(filename,"jcms");
				if(!l.isValidMac()){
					checkLicence = "非法的注册文件，系统不能继续使用，请联系管理员";
					System.out.println(checkLicence);
					return ;				
				}
				
				//3、检查是否过期
				if(!l.isValidDate()){
					checkLicence = "注册文件已过期，系统不能继续使用，请联系管理员";
					System.out.println(checkLicence);
					return ;				
				}
				
				checkLicence = "1";
				//System.out.println(checkLicence);
				
			}catch(Exception e){
				LogWriter.error("checkValid Error:"+e,LoginBLF.class);		
			}
		}	
		/**
		 * 检查用户是否Session是否有效
		 * @param HttpServletRequest reques
		 * @return String
		 */
		public  boolean checkValid(HttpServletRequest request) {
			try{
				boolean b = false;
				if(request.getSession()!=null){
					Merp_Pub_UserEntity u = (Merp_Pub_UserEntity)request.getSession().getAttribute(USERINFO);
					if(u!=null){
						b = true;
					}
				}
				return b;
			}catch(Exception e){
				LogWriter.error("checkValid Error:"+e,LoginBLF.class);
				return false;			
			}
		}	
		
		/**
		 * 检查外网用户Session是否有效
		 * @param HttpServletRequest reques
		 * @return String
		 */
		public  boolean checkValidWeb(HttpServletRequest request) {
			try{
				boolean b = false;
				if(request.getSession()!=null){
					Merp_Pub_UserEntity u = (Merp_Pub_UserEntity)request.getSession().getAttribute(WEBUSERINFO);
					if(u!=null){
						b = true;
					}
				}
				return b;
			}catch(Exception e){
				LogWriter.error("checkWebValid Error:"+e,LoginBLF.class);
				return false;			
			}
		}		
		/**
		 * 获得出错页面
		 * @param HttpServletRequest reques
		 * @param id
		 *            String 出错代号
		 * @return String
		 */
		public  String getErrorUrl(HttpServletRequest request,String id) {
	
			String strUrl = request.getContextPath()
					+ "/oawindow/error/error.jsp?type=" + id;
			return strUrl;
		}	
	}

	String strAppId = "1";
	/**
	 * 检查机构是否存在，不存在则新增
	 * @param entity
	 * @return c_id 机构id
	 */
	private String checkGroup(String groupName){
		if(StringUtils.isBlank(groupName)){
System.out.println("btreceive.jsp checkGroup() groupName为空!");
			LogWriter.debug("btreceive.jsp checkGroup() groupName为空!:");
			return null;
		}
		try{
			String selectcidSql = "SELECT c_id FROM merp_pub_usergroup WHERE"+
			   					  " vc_usergroupname = '"+groupName+"'";
			String[][] result=Manager.doQuery(strAppId,selectcidSql);
			if(result != null && result.length > 0){
				return Convert.getValue(result[0][0]);
			}else{
				String c_id = TableKeyBLF.getStrMaxKey(strAppId,"merp_pub_usergroup", "c_id", 4);
				String insertSql = "INSERT INTO  merp_pub_usergroup "+
								   "(c_id,vc_usergroupname,vc_usergroupspec,"+
								   "vc_parid,vc_extend01,vc_code,i_unitid) VALUES ('"+
								   c_id+"','"+groupName+"','','','','',-11111)";
System.out.println("btreceive.jsp checkGroup insertSql = " + insertSql);
				if(Manager.doExcute(strAppId,insertSql)){
System.out.println(groupName+"机构新增成功!!");
					return c_id;
				}else{
System.out.println(groupName+"机构新增失败!!");
					return "";
				}
			}
		}catch(Exception e){
			LogWriter.debug("btreceive.jsp checkGroup() error:"+e.getMessage());
			return null;
		}
	}
	/**
	 * md5加密
	 * @param str
	 * @return
	 */
	private String md5encode(String str){
		try{
			MD5 md5 = new MD5();
			return md5.encrypt(str, "jcms2008");
			
		}catch(Exception e){
			LogWriter.error("md5encode"+e,UserBLF.class);
			return str;
		}
	}
	//解密
	private String md5decode(String str){
		try{
			MD5 md5 = new MD5();
			return md5.decrypt(str, "jcms2008");
		}catch(Exception e){
			LogWriter.error("md5decode"+e,UserBLF.class);
			return str;			
		}	
	}
	/**
	 * 检查用户是否存在,不存在则新增
	 * @param entity
	 * @return
	 */
	private ArrayList checkUser(Merp_Pub_UserEntity userEntity){
		if(StringUtils.isNotBlank(userEntity.getVc_loginid())){
			ArrayList al = new ArrayList();
			try{
				//根据用户登录名，检查用户是否存在
				UserBLF userBlf = new UserBLF(strAppId,false);
				Merp_Pub_UserEntity user = new Merp_Pub_UserEntity();
				user.setVc_loginid(Convert.getValue(userEntity.getVc_loginid()));
				ArrayList al_user = userBlf.getUserEntity(user);
				if(al_user==null || al_user.size()==0){
					//不存在此用户 则新增
					//先取主键：表中最大的ID ＋ 1
					String c_id = TableKeyBLF.getStrMaxKey(strAppId, "merp_pub_user", "c_id", 5);
					userEntity.setC_id(c_id);
					userEntity.setVc_password("hanweb");
					userEntity.setC_enable("1");
					String vc_loginid = Convert.getValue(userEntity.getVc_loginid());
					//String vc_password = md5encode("hanweb");
					String vc_password = "cW4BYgIdCgcAEXBk";
					String insertSql = "INSERT INTO merp_pub_user (c_id,vc_loginid," +
									   "vc_password,vc_usergroupid,vc_username,c_enable) VALUES ('"+c_id+"','"+
									   vc_loginid +"','"+vc_password+"','"+
									   Convert.getValue(userEntity.getVc_usergroupid())+"','"+
									   Convert.getValue(userEntity.getVc_username())+"','1')";
System.out.println("btreceive.jsp checkUser insertSql = " + insertSql);					
					if(Manager.doExcute(strAppId,insertSql)){
System.out.println(vc_loginid+"用户新增成功!!");
						//新增角色
						if(!insertRole(c_id)){
							return null;
						}
					}else{
						return null;
					}
				}else{
					//以下部分非大汉修改，教育局信息办修改代码，实现如用户已经存在更新用户所在单位（部门）
                       String updateSql="update merp_pub_user set vc_usergroupid= '"+Convert.getValue(userEntity.getVc_usergroupid())+"' where vc_loginid='"+Convert.getValue(userEntity.getVc_loginid())+"'";
					   if(Manager.doExcute(strAppId,updateSql)){
System.out.println("用户修改成功!!");
					
					}else{
						return null;
					}
					//代码结束
System.out.println("已存在用户"+userEntity.getVc_loginid());					
				}
				al.add(userEntity);
				return al;
			}catch(Exception e){
				LogWriter.debug("btreceive.jsp checkUser() error:"+e.getMessage());
				al.add(userEntity);
				return al;
			}
		}else{
			LogWriter.debug("btreceive.jsp checkUser() longid为空!");
			return null;
		}
	}
		
	/**
	 * 增加角色,默认为主站的信息管理员
	 * @param userid
	 * @return
	 */
	private boolean insertRole(String userid){
		if(StringUtils.isBlank(userid)){
			LogWriter.debug("btreceive.jsp insertRole() 传入参数为空!");
			return false;
		}
		try{
			String insertSql = "INSERT INTO merp_pub_roledetail"+
			  				   "(c_roleid,vc_userorgroup,c_type)"+
			                   " VALUES ('0003','"+userid+"','0')";
			return Manager.doExcute(strAppId,insertSql);
		}catch(Exception e){
			LogWriter.debug("btreceive.jsp insertRole() error:"+e.getMessage());
			return false;
		}
	}
	
	private String getTokenURL(HttpServletRequest request,HttpServletResponse response) {
		//接口地址
		String BackURL = "http://www.btedu.gov.cn/jcms/interface/ldap/btreceive.jsp";
		//系统的唯一标识id
		String sys_id = "jcms";
		Pattern p = Pattern.compile("^.*\\?.+=.+$");
		Matcher m = p.matcher(BackURL);
		if (m.matches()) {
			BackURL += "&Token=$Token$";
		} else {
			BackURL += "?Token=$Token$";
		}

		return "http://i.btedu.gov.cn/ssodemo/gettoken.aspx?BackURL=" + BackURL + "&sys_id=" + sys_id;
	}

	private String replaceToken(HttpServletRequest request, HttpServletResponse response) {
		//接口地址
		String BackURL = "http://www.btedu.gov.cn/jcms/interface/ldap/btreceive.jsp";
		//系统的唯一标识id
		String sys_id = "jcms";

		return " http://i.btedu.gov.cn/ssodemo/index.aspx?BackURL=" + BackURL + "&sys_id=" + sys_id;
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
			BackURL = "http://www.btedu.gov.cn/jcms/interface/ldap/btreceive.jsp";
		}
		//系统的唯一标识id
//		String sys_id = "jcms";
/*		
		Pattern p = Pattern.compile("^.*\\?.+=.+$");
		Matcher m = p.matcher(BackURL);
		if (m.matches()) {
			BackURL += "&Token=$Token$";
		} else {
			BackURL += "?Token=$Token$";
		}
*/		
		//strReURL = "http://i.btedu.gov.cn/ssodemo/gettoken.aspx?BackURL=" + BackURL + "&sys_id=" + sys_id;
		strReURL = "http://i.btedu.gov.cn/ssodemo/gettoken.aspx?BackURL=" + BackURL;
//System.out.println("btreceive.jsp getTokenURL strReURL0 = " + strReURL);
//		strReURL = getURL(strReURL);
//System.out.println("btreceive.jsp getTokenURL strReURL1 = " + strReURL);
		return strReURL;
	}

	private String replaceToken() {
		String strReURL = "";
		//接口地址
		String BackURL = "http://www.btedu.gov.cn/jcms/interface/ldap/btreceive.jsp";
		//系统的唯一标识id
		//String sys_id = "jcms";
		//strReURL = " http://i.btedu.gov.cn/ssodemo/index.aspx?BackURL=" + BackURL + "&sys_id=" + sys_id;
		strReURL = " http://i.btedu.gov.cn/ssodemo/index.aspx?BackURL=" + BackURL;
//System.out.println("btreceive.jsp replaceToken strReURL0 = " + strReURL);
//		strReURL = getURL(strReURL);
//System.out.println("btreceive.jsp getTokenURL strReURL1 = " + strReURL);
		return strReURL;
	}

	public class CallNetServices {
		private EndpointReference targetEPR = new EndpointReference("http://i.btedu.gov.cn/SSOdemo/TokenService.asmx");
		//private EndpointReference targetEPR = new EndpointReference("http://i.btedu.gov.cn/web/Data.asmx");
		public String[] getResult(String tokenValue, String mial) throws Exception {
			ServiceClient sender = new ServiceClient();
			sender.setOptions(buildOptions());
			OMElement result = sender.sendReceive(buildParam(tokenValue, "mial"));
//System.out.println("-------------"+result);
			String strxml = result.toString();
System.out.println("strxml="+strxml);
			String[] strArr = getResult(strxml);
if(strArr != null && strArr.length > 0){
	for(int i = 0; i < strArr.length; i++){
		System.out.println("strArr["+i+"]="+strArr[i]);
	}
}
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
			//System.out.println(data);
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
//	System.out.println("Test.getResult() " + strXMLs[i]);    			
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
	LdapBLF ldapBlf = new LdapBLF(request, strAppId, path, "0");
	//获取传递过来的Token值
				String tokenValue = (String) request.getParameter("OnlineId");
     //如果值不存在则跳转到url，请求登录
	if (tokenValue!= null){
//System.out.println("btreceive.jsp session.getAttribute is null...");
		//获取令牌
		String Token = Convert.getParameter(request,"OnlineId","",true,true);
//System.out.println("btreceive.jsp Token = " + Token);		
		if (Token != null && Token.trim().length() > 0) {
//System.out.println("000");
			//将令牌保存到session中
			//session.setAttribute("Token", Token);
			
			//调用WebService获取主站凭证
			//CallNetServices s = new CallNetServices();
			//调WebService返回的值
			/*result返回的是不是以下内容？
				result[0]		用户id
				result[1]    统一用户号 
				result[2]    用户姓名
				result[3]    单位ID
				result[4]    单位名称
				result[5]    类型 1 同步创建，0 不同步
			 */
            
			String[] result = new String[6];
			result[0] = (String) request.getParameter("AutoId");
			result[1] = Convert.getParameter(request,"Account","",true,true);
			result[3] = Convert.getParameter(request,"RealName","",true,true);
			result[5] = Convert.getParameter(request,"DepartmentName","",true,true);
System.out.println("111："+result);
if(result != null && result.length > 0){
	for(int i = 0; i < result.length; i++){
		System.out.println("result["+i+"]="+result[i]);//必要时修改为out语句直接页面输出查询接口获取字段值
	}
}
			if (result[0] != null && result[1] != null) {
System.out.println("btreceive.jsp result.size = " + result.length);
				//需要验证用户及其机构在jcms中是否存在，如果不存在就要新建同步?如果存在就登录到jcms系统中
				String loginId = Convert.getValue(result[1]);
				String userName = Convert.getValue(result[3]);
				String groupName = Convert.getValue(result[5]);
System.out.println("btreceive.jsp loginId = " + loginId);
System.out.println("btreceive.jsp userName = " + userName);
System.out.println("btreceive.jsp groupName = " + groupName);
				Merp_Pub_UserEntity userEntity1 = new Merp_Pub_UserEntity();
				userEntity1.setVc_loginid(loginId);
				userEntity1.setVc_username(userName);
				
				//获取机构id
				String userGroupId = checkGroup(groupName);
System.out.println("btreceive.jsp userGroupId = " + userGroupId);
				userGroupId = Convert.getValue(userGroupId);
				userEntity1.setVc_usergroupid(userGroupId);
				userEntity1.setVc_grouprangename(groupName);
				
				//检测用户是否存在，不在就新增
				Merp_Pub_UserEntity userEntity2 = new Merp_Pub_UserEntity();
				ArrayList al = checkUser(userEntity1);
System.out.println("222");

				if (al != null && al.size() > 0) {
					userEntity2 = (Merp_Pub_UserEntity) al.get(0);
System.out.println("userEntity2 loginid==="+userEntity2.getVc_loginid());
System.out.println("userEntity2 userName==="+userEntity2.getVc_username());
					//JCMS登录验证
					String checkResult = loginBLF.validateLogin(userEntity2, "0");
System.out.println("btreceive.jsp checkResult1 = "+checkResult);
					//用户名密码不对
					if (checkResult.equals("0")) {
						strAlert = "alert('登录名或密码不正确，请重新登录！');\n";
						out.println(Convert.getAlterScript(strAlert));
						out.println(Convert.getAlterScript("location.href='http://www.btedu.gov.cn/jcms/index_jcms.jsp';"));
					//用户没有登录后台权限
					} else if (checkResult.equals("-1")) {
							strAlert = "alert('没有操作权限，请与管理员联系！');\n";
							out.println(Convert.getAlterScript(strAlert));
							out.println(Convert.getAlterScript("location.href='http://www.btedu.gov.cn/jcms/index_jcms.jsp';"));
					//登录到待办事项
					} else {
							out.println(Convert.getAlterScript("location.href='http://www.btedu.gov.cn/jcms/oawindow/main.jsp';"));
					}
				//jcms没有此用户且新增用户失败	
				} else {
System.out.println("btreceive.jsp user is not exist in JCMS");					
				}
System.out.println("333");	
		//将用户信息保存到session中
				session.setAttribute("user", userEntity2);
System.out.println("444");	
			} else {
System.out.println("btreceive.jsp Token got error...");
				//令牌错误，跳转
				//response.sendRedirect(replaceToken(request, response));
				response.sendRedirect("http://i.btedu.gov.cn/Sso/Go/SignOn?Permanent=Jcms");
			}
		} else {
System.out.println("btreceive.jsp Token is null...");
			//未进行令牌验证，跳转验证
			//response.sendRedirect(getTokenURL(request, response));
			response.sendRedirect("http://i.btedu.gov.cn/Sso/Go/SignOn?Permanent=Jcms");
		}
	}else{
	response.sendRedirect("http://i.btedu.gov.cn/Sso/Go/SignOn?Permanent=Jcms");
	}
%>