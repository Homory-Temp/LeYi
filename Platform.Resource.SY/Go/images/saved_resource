/*
 SeaJS - A Module Loader for the Web
 v1.3.0 | seajs.org | MIT Licensed
*/
this.seajs={_seajs:this.seajs};seajs.version="1.3.0";seajs._util={};seajs._config={debug:"",preload:[]};
(function(a){var b=Object.prototype.toString,d=Array.prototype;a.isString=function(a){return"[object String]"===b.call(a)};a.isFunction=function(a){return"[object Function]"===b.call(a)};a.isRegExp=function(a){return"[object RegExp]"===b.call(a)};a.isObject=function(a){return a===Object(a)};a.isArray=Array.isArray||function(a){return"[object Array]"===b.call(a)};a.indexOf=d.indexOf?function(a,b){return a.indexOf(b)}:function(a,b){for(var c=0;c<a.length;c++)if(a[c]===b)return c;return-1};var c=a.forEach=
d.forEach?function(a,b){a.forEach(b)}:function(a,b){for(var c=0;c<a.length;c++)b(a[c],c,a)};a.map=d.map?function(a,b){return a.map(b)}:function(a,b){var d=[];c(a,function(a,c,e){d.push(b(a,c,e))});return d};a.filter=d.filter?function(a,b){return a.filter(b)}:function(a,b){var d=[];c(a,function(a,c,e){b(a,c,e)&&d.push(a)});return d};var e=a.keys=Object.keys||function(a){var b=[],c;for(c in a)a.hasOwnProperty(c)&&b.push(c);return b};a.unique=function(a){var b={};c(a,function(a){b[a]=1});return e(b)};
a.now=Date.now||function(){return(new Date).getTime()}})(seajs._util);(function(a){a.log=function(){if("undefined"!==typeof console){var a=Array.prototype.slice.call(arguments),d="log";console[a[a.length-1]]&&(d=a.pop());if("log"!==d||seajs.debug)if(console[d].apply)console[d].apply(console,a);else{var c=a.length;if(1===c)console[d](a[0]);else if(2===c)console[d](a[0],a[1]);else if(3===c)console[d](a[0],a[1],a[2]);else console[d](a.join(" "))}}}})(seajs._util);
(function(a,b,d){function c(a){a=a.match(p);return(a?a[0]:".")+"/"}function e(a){f.lastIndex=0;f.test(a)&&(a=a.replace(f,"$1/"));if(-1===a.indexOf("."))return a;for(var b=a.split("/"),c=[],d,e=0;e<b.length;e++)if(d=b[e],".."===d){if(0===c.length)throw Error("The path is invalid: "+a);c.pop()}else"."!==d&&c.push(d);return c.join("/")}function l(a){var a=e(a),b=a.charAt(a.length-1);if("/"===b)return a;"#"===b?a=a.slice(0,-1):-1===a.indexOf("?")&&!k.test(a)&&(a+=".js");0<a.indexOf(":80/")&&(a=a.replace(":80/",
"/"));return a}function g(a){if("#"===a.charAt(0))return a.substring(1);var c=b.alias;if(c&&r(a)){var d=a.split("/"),e=d[0];c.hasOwnProperty(e)&&(d[0]=c[e],a=d.join("/"))}return a}function i(a){return 0<a.indexOf("://")||0===a.indexOf("//")}function m(a){return"/"===a.charAt(0)&&"/"!==a.charAt(1)}function r(a){var b=a.charAt(0);return-1===a.indexOf("://")&&"."!==b&&"/"!==b}var p=/.*(?=\/.*$)/,f=/([^:\/])\/\/+/g,k=/\.(?:css|js)$/,o=/^(.*?\w)(?:\/|$)/,j={},d=d.location,q=d.protocol+"//"+d.host+function(a){"/"!==
a.charAt(0)&&(a="/"+a);return a}(d.pathname);0<q.indexOf("\\")&&(q=q.replace(/\\/g,"/"));a.dirname=c;a.realpath=e;a.normalize=l;a.parseAlias=g;a.parseMap=function(d){var f=b.map||[];if(!f.length)return d;for(var n=d,m=0;m<f.length;m++){var h=f[m];if(a.isArray(h)&&2===h.length){var g=h[0];if(a.isString(g)&&-1<n.indexOf(g)||a.isRegExp(g)&&g.test(n))n=n.replace(g,h[1])}else a.isFunction(h)&&(n=h(n))}i(n)||(n=e(c(q)+n));n!==d&&(j[n]=d);return n};a.unParseMap=function(a){return j[a]||a};a.id2Uri=function(a,
d){if(!a)return"";a=g(a);d||(d=q);var e;i(a)?e=a:0===a.indexOf("./")||0===a.indexOf("../")?(0===a.indexOf("./")&&(a=a.substring(2)),e=c(d)+a):e=m(a)?d.match(o)[1]+a:b.base+"/"+a;return l(e)};a.isAbsolute=i;a.isRoot=m;a.isTopLevel=r;a.pageUri=q})(seajs._util,seajs._config,this);
(function(a,b){function d(a,c){a.onload=a.onerror=a.onreadystatechange=function(){p.test(a.readyState)&&(a.onload=a.onerror=a.onreadystatechange=null,a.parentNode&&!b.debug&&i.removeChild(a),a=void 0,c())}}function c(b,c){j||q?(a.log("Start poll to fetch css"),setTimeout(function(){e(b,c)},1)):b.onload=b.onerror=function(){b.onload=b.onerror=null;b=void 0;c()}}function e(a,b){var c;if(j)a.sheet&&(c=!0);else if(a.sheet)try{a.sheet.cssRules&&(c=!0)}catch(d){"NS_ERROR_DOM_SECURITY_ERR"===d.name&&(c=
!0)}setTimeout(function(){c?b():e(a,b)},1)}function l(){}var g=document,i=g.head||g.getElementsByTagName("head")[0]||g.documentElement,m=i.getElementsByTagName("base")[0],r=/\.css(?:\?|$)/i,p=/loaded|complete|undefined/,f,k;a.fetch=function(b,e,j){var g=r.test(b),h=document.createElement(g?"link":"script");j&&(j=a.isFunction(j)?j(b):j)&&(h.charset=j);e=e||l;"SCRIPT"===h.nodeName?d(h,e):c(h,e);g?(h.rel="stylesheet",h.href=b):(h.async="async",h.src=b);f=h;m?i.insertBefore(h,m):i.appendChild(h);f=null};
a.getCurrentScript=function(){if(f)return f;if(k&&"interactive"===k.readyState)return k;for(var a=i.getElementsByTagName("script"),b=0;b<a.length;b++){var c=a[b];if("interactive"===c.readyState)return k=c}};a.getScriptAbsoluteSrc=function(a){return a.hasAttribute?a.src:a.getAttribute("src",4)};a.importStyle=function(a,b){if(!b||!g.getElementById(b)){var c=g.createElement("style");b&&(c.id=b);i.appendChild(c);c.styleSheet?c.styleSheet.cssText=a:c.appendChild(g.createTextNode(a))}};var o=navigator.userAgent,
j=536>Number(o.replace(/.*AppleWebKit\/(\d+)\..*/,"$1")),q=0<o.indexOf("Firefox")&&!("onload"in document.createElement("link"))})(seajs._util,seajs._config,this);(function(a){var b=/(?:^|[^.$])\brequire\s*\(\s*(["'])([^"'\s\)]+)\1\s*\)/g;a.parseDependencies=function(d){var c=[],e,d=d.replace(/^\s*\/\*[\s\S]*?\*\/\s*$/mg,"").replace(/^\s*\/\/.*$/mg,"");for(b.lastIndex=0;e=b.exec(d);)e[2]&&c.push(e[2]);return a.unique(c)}})(seajs._util);
(function(a,b,d){function c(a,b){this.uri=a;this.status=b||0}function e(a,d){return b.isString(a)?c._resolve(a,d):b.map(a,function(a){return e(a,d)})}function l(a,t){var e=b.parseMap(a);v[e]?(f[a]=f[e],t()):q[e]?u[e].push(t):(q[e]=!0,u[e]=[t],c._fetch(e,function(){v[e]=!0;var d=f[a];d.status===j.FETCHING&&(d.status=j.FETCHED);n&&(c._save(a,n),n=null);s&&d.status===j.FETCHED&&(f[a]=s,s.realUri=a);s=null;q[e]&&delete q[e];if(d=u[e])delete u[e],b.forEach(d,function(a){a()})},d.charset))}function g(a,
b){var c=a(b.require,b.exports,b);void 0!==c&&(b.exports=c)}function i(a){var c=a.realUri||a.uri,d=k[c];d&&(b.forEach(d,function(b){g(b,a)}),delete k[c])}function m(a){var c=a.uri;return b.filter(a.dependencies,function(a){h=[c];if(a=r(f[a]))h.push(c),b.log("Found circular dependencies:",h.join(" --\> "),void 0);return!a})}function r(a){if(!a||a.status!==j.SAVED)return!1;h.push(a.uri);a=a.dependencies;if(a.length){var c=a.concat(h);if(c.length>b.unique(c).length)return!0;for(c=0;c<a.length;c++)if(r(f[a[c]]))return!0}h.pop();
return!1}function p(a){var b=d.preload.slice();d.preload=[];b.length?w._use(b,a):a()}var f={},k={},o=[],j={FETCHING:1,FETCHED:2,SAVED:3,READY:4,COMPILING:5,COMPILED:6};c.prototype._use=function(a,c){b.isString(a)&&(a=[a]);var d=e(a,this.uri);this._load(d,function(){p(function(){var a=b.map(d,function(a){return a?f[a]._compile():null});c&&c.apply(null,a)})})};c.prototype._load=function(a,d){function e(a){(a||{}).status<j.READY&&(a.status=j.READY);0===--i&&d()}var x=b.filter(a,function(a){return a&&
(!f[a]||f[a].status<j.READY)}),g=x.length;if(0===g)d();else for(var i=g,h=0;h<g;h++)(function(a){function b(){d=f[a];if(d.status>=j.SAVED){var t=m(d);t.length?c.prototype._load(t,function(){e(d)}):e(d)}else e()}var d=f[a]||(f[a]=new c(a,j.FETCHING));d.status>=j.FETCHED?b():l(a,b)})(x[h])};c.prototype._compile=function(){function a(b){b=e(b,c.uri);b=f[b];if(!b)return null;if(b.status===j.COMPILING)return b.exports;b.parent=c;return b._compile()}var c=this;if(c.status===j.COMPILED)return c.exports;
if(c.status<j.SAVED&&!k[c.realUri||c.uri])return null;c.status=j.COMPILING;a.async=function(a,b){c._use(a,b)};a.resolve=function(a){return e(a,c.uri)};a.cache=f;c.require=a;c.exports={};var d=c.factory;b.isFunction(d)?(o.push(c),g(d,c),o.pop()):void 0!==d&&(c.exports=d);c.status=j.COMPILED;i(c);return c.exports};c._define=function(a,d,g){var h=arguments.length;1===h?(g=a,a=void 0):2===h&&(g=d,d=void 0,b.isArray(a)&&(d=a,a=void 0));!b.isArray(d)&&b.isFunction(g)&&(d=b.parseDependencies(g.toString()));
var h={id:a,dependencies:d,factory:g},i;if(document.attachEvent){var m=b.getCurrentScript();m&&(i=b.unParseMap(b.getScriptAbsoluteSrc(m)));i||b.log("Failed to derive URI from interactive script for:",g.toString(),"warn")}if(m=a?e(a):i){if(m===i){var k=f[i];k&&(k.realUri&&k.status===j.SAVED)&&(f[i]=null)}h=c._save(m,h);if(i){if((f[i]||{}).status===j.FETCHING)f[i]=h,h.realUri=i}else s||(s=h)}else n=h};c._getCompilingModule=function(){return o[o.length-1]};c._find=function(a){var c=[];b.forEach(b.keys(f),
function(d){if(b.isString(a)&&-1<d.indexOf(a)||b.isRegExp(a)&&a.test(d))d=f[d],d.exports&&c.push(d.exports)});return c};c._modify=function(b,c){var d=e(b),i=f[d];i&&i.status===j.COMPILED?g(c,i):(k[d]||(k[d]=[]),k[d].push(c));return a};c.STATUS=j;c._resolve=b.id2Uri;c._fetch=b.fetch;c._save=function(a,d){var i=f[a]||(f[a]=new c(a));i.status<j.SAVED&&(i.id=d.id||a,i.dependencies=e(b.filter(d.dependencies||[],function(a){return!!a}),a),i.factory=d.factory,i.status=j.SAVED);return i};var q={},v={},u=
{},n=null,s=null,h=[],w=new c(b.pageUri,j.COMPILED);a.use=function(b,c){p(function(){w._use(b,c)});return a};a.define=c._define;a.cache=c.cache=f;a.find=c._find;a.modify=c._modify;c.fetchedList=v;a.pluginSDK={Module:c,util:b,config:d}})(seajs,seajs._util,seajs._config);
(function(a,b,d){var c="seajs-ts="+b.now(),e=document.getElementById("seajsnode");e||(e=document.getElementsByTagName("script"),e=e[e.length-1]);var l=e&&b.getScriptAbsoluteSrc(e)||b.pageUri,l=b.dirname(function(a){if(a.indexOf("??")===-1)return a;var c=a.split("??"),a=c[0],c=b.filter(c[1].split(","),function(a){return a.indexOf("sea.js")!==-1});return a+c[0]}(l));b.loaderDir=l;var g=l.match(/^(.+\/)seajs\/[\.\d]+(?:-dev)?\/$/);g&&(l=g[1]);d.base=l;d.main=e&&e.getAttribute("data-main");d.charset=
"utf-8";a.config=function(e){for(var g in e)if(e.hasOwnProperty(g)){var l=d[g],p=e[g];if(l&&g==="alias")for(var f in p){if(p.hasOwnProperty(f)){var k=l[f],o=p[f];/^\d+\.\d+\.\d+$/.test(o)&&(o=f+"/"+o+"/"+f);k&&k!==o&&b.log("The alias config is conflicted:","key =",'"'+f+'"',"previous =",'"'+k+'"',"current =",'"'+o+'"',"warn");l[f]=o}}else if(l&&(g==="map"||g==="preload")){b.isString(p)&&(p=[p]);b.forEach(p,function(a){a&&l.push(a)})}else d[g]=p}if((e=d.base)&&!b.isAbsolute(e))d.base=b.id2Uri((b.isRoot(e)?
"":"./")+e+"/");if(d.debug===2){d.debug=1;a.config({map:[[/^.*$/,function(a){a.indexOf("seajs-ts=")===-1&&(a=a+((a.indexOf("?")===-1?"?":"&")+c));return a}]]})}if(d.debug)a.debug=!!d.debug;return this};d.debug&&(a.debug=!!d.debug)})(seajs,seajs._util,seajs._config);
(function(a,b,d){a.log=b.log;a.importStyle=b.importStyle;a.config({alias:{seajs:b.loaderDir}});b.forEach(function(){var a=[],e=d.location.search,e=e.replace(/(seajs-\w+)(&|$)/g,"$1=1$2"),e=e+(" "+document.cookie);e.replace(/seajs-(\w+)=[1-9]/g,function(b,d){a.push(d)});return b.unique(a)}(),function(b){a.use("seajs/plugin-"+b);"debug"===b&&(a._use=a.use,a._useArgs=[],a.use=function(){a._useArgs.push(arguments);return a})})})(seajs,seajs._util,this);
(function(a,b,d){var c=a._seajs;if(c&&!c.args)d.seajs=a._seajs;else{d.define=a.define;b.main&&a.use(b.main);if(b=(c||0).args)for(var c={"0":"config",1:"use",2:"define"},e=0;e<b.length;e+=2)a[c[b[e]]].apply(a,b[e+1]);d.define.cmd={};delete a.define;delete a._util;delete a._config;delete a._seajs}})(seajs,seajs._config,this);
define("seajs/plugin-base",[],function(m,k){var l=seajs.pluginSDK,i=l.util,h=l.Module,g={},j={};k.add=function(b){g[b.name]=b};k.util={xhr:function(b,a){var c=window.ActiveXObject?new window.ActiveXObject("Microsoft.XMLHTTP"):new window.XMLHttpRequest;c.open("GET",b,!0);c.onreadystatechange=function(){if(4===c.readyState)if(200===c.status)a(c.responseText);else throw Error("Could not load: "+b+", status = "+c.status);};return c.send(null)},globalEval:function(b){b&&/\S/.test(b)&&(window.execScript||
function(a){window.eval.call(window,a)})(b)}};(function(){var b=h._resolve;h._resolve=function(a,c){var d,e;if(e=a.match(/^(\w+)!(.+)$/))d=e[1],a=e[2];a="#"+i.parseAlias(a);if(!d&&(e=a.match(/[^?]*(\.\w+)/))){e=e[1];for(var f in g)if(g.hasOwnProperty(f)&&-1<i.indexOf(g[f].ext,e)){d=f;break}}d&&!/\?|#$/.test(a)&&(a+="#");f=b(a,c);d&&(g[d]&&!j[f])&&(j[f]=d);return f}})();(function(){var b=h._fetch;h._fetch=function(a,c,d){var e=j[i.unParseMap(a)];e?g[e].fetch(a,c,d):b(a,c,d)}})()});
/**
 * The combo plugin for http concat module
 */
define('seajs/plugin-combo', [], function() {

  var pluginSDK = seajs.pluginSDK
  var util = pluginSDK.util
  var config = pluginSDK.config


  // Hacks load function to inject combo support
  // -----------------------------------------------

  var Module = pluginSDK.Module
  var cachedModules = seajs.cache


  function hackLoad() {
    var MP = Module.prototype
    var _load = MP._load

    MP._load = function(uris, callback) {
      setComboMap(uris)
      _load.call(this, uris, callback)
    }
  }


  function setComboMap(uris) {
    var comboExcludes = config.comboExcludes

    // Removes fetched or fetching uri
    var unFetchingUris = util.filter(uris, function(uri) {
      var module = cachedModules[uri]

      return (!module || module.status < Module.STATUS.FETCHING) &&
          (!comboExcludes || !comboExcludes.test(uri))
    })

    if (unFetchingUris.length > 1) {
      seajs.config({ map: paths2map(uris2paths(unFetchingUris)) })
    }
  }


  // No combo in debug mode
  if (seajs.debug) {
    seajs.log('Combo is turned off in debug mode')
  } else {
    hackLoad()
  }


  // Uses map to implement combo support
  // -----------------------------------------------

  function uris2paths(uris) {
    return meta2paths(uris2meta(uris))
  }

  // [
  //   'http://example.com/p/a.js',
  //   'https://example2.com/b.js',
  //   'http://example.com/p/c/d.js',
  //   'http://example.com/p/c/e.js'
  // ]
  // ==>
  // {
  //   'http__example.com': {
  //                          'p': {
  //                                 'a.js': { __KEYS: [] },
  //                                 'c': {
  //                                        'd.js': { __KEYS: [] },
  //                                        'e.js': { __KEYS: [] },
  //                                        __KEYS: ['d.js', 'e.js']
  //                                 },
  //                                 __KEYS: ['a.js', 'c']
  //                               },
  //                          __KEYS: ['p']
  //                        },
  //   'https__example2.com': {
  //                            'b.js': { __KEYS: [] },
  //                            _KEYS: ['b.js']
  //                          },
  //   __KEYS: ['http__example.com', 'https__example2.com']
  // }
  function uris2meta(uris) {
    var meta = { __KEYS: [] }

    util.forEach(uris, function(uri) {
      var parts = uri.replace('://', '__').split('/')
      var m = meta

      util.forEach(parts, function(part) {
        if (!m[part]) {
          m[part] = { __KEYS: [] }
          m.__KEYS.push(part)
        }
        m = m[part]
      })

    })

    return meta
  }


  // {
  //   'http__example.com': {
  //                          'p': {
  //                                 'a.js': { __KEYS: [] },
  //                                 'c': {
  //                                        'd.js': { __KEYS: [] },
  //                                        'e.js': { __KEYS: [] },
  //                                        __KEYS: ['d.js', 'e.js']
  //                                 },
  //                                 __KEYS: ['a.js', 'c']
  //                               },
  //                          __KEYS: ['p']
  //                        },
  //   'https__example2.com': {
  //                            'b.js': { __KEYS: [] },
  //                            _KEYS: ['b.js']
  //                          },
  //   __KEYS: ['http__example.com', 'https__example2.com']
  // }
  // ==>
  // [
  //   ['http://example.com/p', ['a.js', 'c/d.js', 'c/e.js']]
  // ]
  function meta2paths(meta) {
    var paths = []

    util.forEach(meta.__KEYS, function(part) {
      var root = part
      var m = meta[part]
      var KEYS = m.__KEYS

      while(KEYS.length === 1) {
        root += '/' + KEYS[0]
        m = m[KEYS[0]]
        KEYS = m.__KEYS
      }

      if (KEYS.length) {
        paths.push([root.replace('__', '://'), meta2arr(m)])
      }
    })

    return paths
  }


  // {
  //   'a.js': { __KEYS: [] },
  //   'c': {
  //          'd.js': { __KEYS: [] },
  //          'e.js': { __KEYS: [] },
  //          __KEYS: ['d.js', 'e.js']
  //        },
  //   __KEYS: ['a.js', 'c']
  // }
  // ==>
  // [
  //   'a.js', 'c/d.js', 'c/e.js'
  // ]
  function meta2arr(meta) {
    var arr = []

    util.forEach(meta.__KEYS, function(key) {
      var r = meta2arr(meta[key])

      // key = 'c'
      // r = ['d.js', 'e.js']
      if (r.length) {
        util.forEach(r, function(part) {
          arr.push(key + '/' + part)
        })
      }
      else {
        arr.push(key)
      }
    })

    return arr
  }


  // [
  //   [ 'http://example.com/p', ['a.js', 'c/d.js', 'c/e.js', 'a.css', 'b.css'] ]
  // ]
  // ==>
  //
  // a map function to map
  //
  // 'http://example.com/p/a.js'  ==> 'http://example.com/p/??a.js,c/d.js,c/e.js'
  // 'http://example.com/p/c/d.js'  ==> 'http://example.com/p/??a.js,c/d.js,c/e.js'
  // 'http://example.com/p/c/e.js'  ==> 'http://example.com/p/??a.js,c/d.js,c/e.js'
  // 'http://example.com/p/a.css'  ==> 'http://example.com/p/??a.css,b.css'
  // 'http://example.com/p/b.css'  ==> 'http://example.com/p/??a.css,b.css'
  //
  function paths2map(paths) {
    var comboSyntax = config.comboSyntax || ['??', ',']
    var map = []

    util.forEach(paths, function(path) {
      var root = path[0] + '/'
      var group = files2group(path[1])

      util.forEach(group, function(files) {

        var hash = {}
        var comboPath = root + comboSyntax[0] + files.join(comboSyntax[1])

        // http://stackoverflow.com/questions/417142/what-is-the-maximum-length-of-a-url
        if (comboPath.length > 2000) {
          throw new Error('The combo url is too long: ' + comboPath)
        }

        util.forEach(files, function(part) {
          hash[root + part] = comboPath
        })

        map.push(function(url) {
          return hash[url] || url
        })

      })

    })

    return map
  }


  //
  //  ['a.js', 'c/d.js', 'c/e.js', 'a.css', 'b.css', 'z']
  // ==>
  //  [ ['a.js', 'c/d.js', 'c/e.js'], ['a.css', 'b.css'] ]
  //
  function files2group(files) {
    var group = []
    var hash = {}

    util.forEach(files, function(file) {
      var ext = getExt(file)
      if (ext) {
        (hash[ext] || (hash[ext] = [])).push(file)
      }
    })

    for (var ext in hash) {
      if (hash.hasOwnProperty(ext)) {
        group.push(hash[ext])
      }
    }

    return group
  }


  function getExt(file) {
    var p = file.lastIndexOf('.')
    return p >= 0 ? file.substring(p) : ''
  }


  // For test
  util.toComboPaths = uris2paths
  util.toComboMap = paths2map

})

// Runs it immediately
seajs.use('seajs/plugin-combo');

/**
 * The text plugin to load a module as text content
 */
define('seajs/plugin-text', ['./plugin-base'], function(require) {

  var plugin = require('./plugin-base')
  var util = plugin.util


  plugin.add({
    name: 'text',

    ext: ['.tpl', '.htm', '.html'],

    fetch: function(url, callback) {
      util.xhr(url, function(data) {
        var str = jsEscape(data)
        util.globalEval('define([], "' + str + '")')
        callback()
      })
    }
  })


  function jsEscape(s) {
    return s.replace(/(["\\])/g, '\\$1')
        .replace(/\r/g, "\\r")
        .replace(/\n/g, "\\n")
        .replace(/\t/g, "\\t")
        .replace(/\f/g, "\\f")
  }

});

var T,baidu=T=baidu||{version:"1.5.2.2"};baidu.guid="$BAIDU$";baidu.$$=window[baidu.guid]=window[baidu.guid]||{global:{}};baidu.ajax=baidu.ajax||{};baidu.fn=baidu.fn||{};baidu.fn.blank=function(){};baidu.ajax.request=function(f,j){var d=j||{},q=d.data||"",g=!(d.async===false),e=d.username||"",a=d.password||"",c=(d.method||"GET").toUpperCase(),b=d.headers||{},i=d.timeout||0,k={},n,r,h;function m(){if(h.readyState==4){try{var t=h.status}catch(s){p("failure");return}p(t);if((t>=200&&t<300)||t==304||t==1223){p("success")}else{p("failure")}window.setTimeout(function(){h.onreadystatechange=baidu.fn.blank;if(g){h=null}},0)}}function l(){if(window.ActiveXObject){try{return new ActiveXObject("Msxml2.XMLHTTP")}catch(s){try{return new ActiveXObject("Microsoft.XMLHTTP")}catch(s){}}}if(window.XMLHttpRequest){return new XMLHttpRequest()}}function p(u){u="on"+u;var t=k[u],v=baidu.ajax[u];if(t){if(n){clearTimeout(n)}if(u!="onsuccess"){t(h)}else{try{h.responseText}catch(s){return t(h)}t(h,h.responseText)}}else{if(v){if(u=="onsuccess"){return}v(h)}}}for(r in d){k[r]=d[r]}b["X-Requested-With"]="XMLHttpRequest";try{h=l();if(c=="GET"){if(q){f+=(f.indexOf("?")>=0?"&":"?")+q;q=null}if(d.noCache){f+=(f.indexOf("?")>=0?"&":"?")+"b"+(+new Date)+"=1"}}if(e){h.open(c,f,g,e,a)}else{h.open(c,f,g)}if(g){h.onreadystatechange=m}if(c=="POST"){h.setRequestHeader("Content-Type",(b["Content-Type"]||"application/x-www-form-urlencoded"))}for(r in b){if(b.hasOwnProperty(r)){h.setRequestHeader(r,b[r])}}p("beforerequest");if(i){n=setTimeout(function(){h.onreadystatechange=baidu.fn.blank;h.abort();p("timeout")},i)}h.send(q);if(!g){m()}}catch(o){p("failure")}return h};baidu.ajax.get=function(b,a){return baidu.ajax.request(b,{onsuccess:a})};baidu.ajax.post=function(b,c,a){return baidu.ajax.request(b,{onsuccess:a,method:"POST",data:c})};baidu.array=baidu.array||{};baidu.each=baidu.array.forEach=baidu.array.each=function(g,e,b){var d,f,c,a=g.length;if("function"==typeof e){for(c=0;c<a;c++){f=g[c];d=e.call(b||g,f,c);if(d===false){break}}}return g};baidu.array.filter=function(h,f,d){var c=[],b=0,a=h.length,g,e;if("function"==typeof f){for(e=0;e<a;e++){g=h[e];if(true===f.call(d||h,g,e)){c[b++]=g}}}return c};baidu.array.indexOf=function(e,b,d){var a=e.length,c=b;d=d|0;if(d<0){d=Math.max(0,a+d)}for(;d<a;d++){if(d in e&&e[d]===b){return d}}return -1};baidu.array.remove=function(c,b){var a=c.length;while(a--){if(a in c&&c[a]===b){c.splice(a,1)}}return c};baidu.array.removeAt=function(b,a){return b.splice(a,1)[0]};baidu.browser=baidu.browser||{};baidu.browser.firefox=/firefox\/(\d+\.\d+)/i.test(navigator.userAgent)?+RegExp["\x241"]:undefined;baidu.browser.ie=baidu.ie=/msie (\d+\.\d+)/i.test(navigator.userAgent)?(document.documentMode||+RegExp["\x241"]):undefined;baidu.browser.isGecko=/gecko/i.test(navigator.userAgent)&&!/like gecko/i.test(navigator.userAgent);baidu.browser.isStrict=document.compatMode=="CSS1Compat";baidu.browser.isWebkit=/webkit/i.test(navigator.userAgent);baidu.browser.opera=/opera(\/| )(\d+(\.\d+)?)(.+?(version\/(\d+(\.\d+)?)))?/i.test(navigator.userAgent)?+(RegExp["\x246"]||RegExp["\x242"]):undefined;(function(){var a=navigator.userAgent;baidu.browser.safari=/(\d+\.\d)?(?:\.\d)?\s+safari\/?(\d+\.\d+)?/i.test(a)&&!/chrome/i.test(a)?+(RegExp["\x241"]||RegExp["\x242"]):undefined})();baidu.cookie=baidu.cookie||{};baidu.cookie._isValidKey=function(a){return(new RegExp('^[^\\x00-\\x20\\x7f\\(\\)<>@,;:\\\\\\"\\[\\]\\?=\\{\\}\\/\\u0080-\\uffff]+\x24')).test(a)};baidu.cookie.getRaw=function(b){if(baidu.cookie._isValidKey(b)){var c=new RegExp("(^| )"+b+"=([^;]*)(;|\x24)"),a=c.exec(document.cookie);if(a){return a[2]||null}}return null};baidu.cookie.get=function(a){var b=baidu.cookie.getRaw(a);if("string"==typeof b){b=decodeURIComponent(b);return b}return null};baidu.cookie.setRaw=function(c,d,b){if(!baidu.cookie._isValidKey(c)){return}b=b||{};var a=b.expires;if("number"==typeof b.expires){a=new Date();a.setTime(a.getTime()+b.expires)}document.cookie=c+"="+d+(b.path?"; path="+b.path:"")+(a?"; expires="+a.toGMTString():"")+(b.domain?"; domain="+b.domain:"")+(b.secure?"; secure":"")};baidu.cookie.remove=function(b,a){a=a||{};a.expires=new Date(0);baidu.cookie.setRaw(b,"",a)};baidu.cookie.set=function(b,c,a){baidu.cookie.setRaw(b,encodeURIComponent(c),a)};baidu.date=baidu.date||{};baidu.number=baidu.number||{};baidu.number.pad=function(d,c){var e="",b=(d<0),a=String(Math.abs(d));if(a.length<c){e=(new Array(c-a.length+1)).join("0")}return(b?"-":"")+e+a};baidu.date.format=function(a,f){if("string"!=typeof f){return a.toString()}function d(l,k){f=f.replace(l,k)}var b=baidu.number.pad,g=a.getFullYear(),e=a.getMonth()+1,j=a.getDate(),h=a.getHours(),c=a.getMinutes(),i=a.getSeconds();d(/yyyy/g,b(g,4));d(/yy/g,b(parseInt(g.toString().slice(2),10),2));d(/MM/g,b(e,2));d(/M/g,e);d(/dd/g,b(j,2));d(/d/g,j);d(/HH/g,b(h,2));d(/H/g,h);d(/hh/g,b(h%12,2));d(/h/g,h%12);d(/mm/g,b(c,2));d(/m/g,c);d(/ss/g,b(i,2));d(/s/g,i);return f};baidu.date.parse=function(c){var a=new RegExp("^\\d+(\\-|\\/)\\d+(\\-|\\/)\\d+\x24");if("string"==typeof c){if(a.test(c)||isNaN(Date.parse(c))){var f=c.split(/ |T/),b=f.length>1?f[1].split(/[^\d]/):[0,0,0],e=f[0].split(/[^\d]/);return new Date(e[0]-0,e[1]-1,e[2]-0,b[0]-0,b[1]-0,b[2]-0)}else{return new Date(c)}}return new Date()};baidu.dom=baidu.dom||{};baidu.dom._NAME_ATTRS=(function(){var a={cellpadding:"cellPadding",cellspacing:"cellSpacing",colspan:"colSpan",rowspan:"rowSpan",valign:"vAlign",usemap:"useMap",frameborder:"frameBorder"};if(baidu.browser.ie<8){a["for"]="htmlFor";a["class"]="className"}else{a.htmlFor="for";a.className="class"}return a})();baidu.lang=baidu.lang||{};baidu.lang.isString=function(a){return"[object String]"==Object.prototype.toString.call(a)};baidu.isString=baidu.lang.isString;baidu.dom._g=function(a){if(baidu.lang.isString(a)){return document.getElementById(a)}return a};baidu._g=baidu.dom._g;baidu.dom.g=function(a){if(!a){return null}if("string"==typeof a||a instanceof String){return document.getElementById(a)}else{if(a.nodeName&&(a.nodeType==1||a.nodeType==9)){return a}}return null};baidu.g=baidu.G=baidu.dom.g;baidu.dom._matchNode=function(a,c,d){a=baidu.dom.g(a);for(var b=a[d];b;b=b[c]){if(b.nodeType==1){return b}}return null};baidu.dom._styleFilter=baidu.dom._styleFilter||[];baidu.dom._styleFilter[baidu.dom._styleFilter.length]={get:function(c,d){if(/color/i.test(c)&&d.indexOf("rgb(")!=-1){var e=d.split(",");d="#";for(var b=0,a;a=e[b];b++){a=parseInt(a.replace(/[^\d]/gi,""),10).toString(16);d+=a.length==1?"0"+a:a}d=d.toUpperCase()}return d}};baidu.dom._styleFilter.filter=function(b,e,f){for(var a=0,d=baidu.dom._styleFilter,c;c=d[a];a++){if(c=c[f]){e=c(b,e)}}return e};baidu.dom._styleFilter[baidu.dom._styleFilter.length]={set:function(a,b){if(b.constructor==Number&&!/zIndex|fontWeight|opacity|zoom|lineHeight/i.test(a)){b=b+"px"}return b}};baidu.dom._styleFixer=baidu.dom._styleFixer||{};baidu.dom._styleFixer.display=baidu.browser.ie&&baidu.browser.ie<8?{set:function(a,b){a=a.style;if(b=="inline-block"){a.display="inline";a.zoom=1}else{a.display=b}}}:baidu.browser.firefox&&baidu.browser.firefox<3?{set:function(a,b){a.style.display=b=="inline-block"?"-moz-inline-box":b}}:null;baidu.dom._styleFixer["float"]=baidu.browser.ie?"styleFloat":"cssFloat";baidu.dom._styleFixer.opacity=baidu.browser.ie?{get:function(a){var b=a.style.filter;return b&&b.indexOf("opacity=")>=0?(parseFloat(b.match(/opacity=([^)]*)/)[1])/100)+"":"1"},set:function(a,c){var b=a.style;b.filter=(b.filter||"").replace(/alpha\([^\)]*\)/gi,"")+(c==1?"":"alpha(opacity="+c*100+")");b.zoom=1}}:null;baidu.string=baidu.string||{};(function(){var a=new RegExp("(^[\\s\\t\\xa0\\u3000]+)|([\\u3000\\xa0\\s\\t]+\x24)","g");baidu.string.trim=function(b){return String(b).replace(a,"")}})();baidu.trim=baidu.string.trim;baidu.dom.addClass=function(f,g){f=baidu.dom.g(f);var b=g.split(/\s+/),a=f.className,e=" "+a+" ",d=0,c=b.length;for(;d<c;d++){if(e.indexOf(" "+b[d]+" ")<0){a+=(a?" ":"")+b[d]}}f.className=a;return f};baidu.addClass=baidu.dom.addClass;baidu.dom.children=function(b){b=baidu.dom.g(b);for(var a=[],c=b.firstChild;c;c=c.nextSibling){if(c.nodeType==1){a.push(c)}}return a};baidu.dom.contains=function(a,b){var c=baidu.dom._g;a=c(a);b=c(b);return a.contains?a!=b&&a.contains(b):!!(a.compareDocumentPosition(b)&16)};baidu.dom.setAttr=function(b,a,c){b=baidu.dom.g(b);if("style"==a){b.style.cssText=c}else{a=baidu.dom._NAME_ATTRS[a]||a;b.setAttribute(a,c)}return b};baidu.setAttr=baidu.dom.setAttr;baidu.dom.setAttrs=function(c,a){c=baidu.dom.g(c);for(var b in a){baidu.dom.setAttr(c,b,a[b])}return c};baidu.setAttrs=baidu.dom.setAttrs;baidu.dom.create=function(c,a){var d=document.createElement(c),b=a||{};return baidu.dom.setAttrs(d,b)};baidu.object=baidu.object||{};baidu.extend=baidu.object.extend=function(c,a){for(var b in a){if(a.hasOwnProperty(b)){c[b]=a[b]}}return c};baidu.dom.getDocument=function(a){a=baidu.dom.g(a);return a.nodeType==9?a:a.ownerDocument||a.document};baidu.dom.getComputedStyle=function(b,a){b=baidu.dom._g(b);var d=baidu.dom.getDocument(b),c;if(d.defaultView&&d.defaultView.getComputedStyle){c=d.defaultView.getComputedStyle(b,null);if(c){return c[a]||c.getPropertyValue(a)}}return""};baidu.string.toCamelCase=function(a){if(a.indexOf("-")<0&&a.indexOf("_")<0){return a}return a.replace(/[-_][^-_]/g,function(b){return b.charAt(1).toUpperCase()})};baidu.dom.getStyle=function(c,b){var e=baidu.dom;c=e.g(c);b=baidu.string.toCamelCase(b);var d=c.style[b]||(c.currentStyle?c.currentStyle[b]:"")||e.getComputedStyle(c,b);if(!d||d=="auto"){var a=e._styleFixer[b];if(a){d=a.get?a.get(c,b,d):baidu.dom.getStyle(c,a)}}if(a=e._styleFilter){d=a.filter(b,d,"get")}return d};baidu.getStyle=baidu.dom.getStyle;baidu.event=baidu.event||{};baidu.event._listeners=baidu.event._listeners||[];baidu.event.on=function(b,e,g){e=e.replace(/^on/i,"");b=baidu.dom._g(b);var f=function(i){g.call(b,i)},a=baidu.event._listeners,d=baidu.event._eventFilter,h,c=e;e=e.toLowerCase();if(d&&d[e]){h=d[e](b,e,f);c=h.type;f=h.listener}if(b.addEventListener){b.addEventListener(c,f,false)}else{if(b.attachEvent){b.attachEvent("on"+c,f)}}a[a.length]=[b,e,g,f,c];return b};baidu.on=baidu.event.on;baidu.page=baidu.page||{};baidu.page.getScrollTop=function(){var a=document;return window.pageYOffset||a.documentElement.scrollTop||a.body.scrollTop};baidu.page.getScrollLeft=function(){var a=document;return window.pageXOffset||a.documentElement.scrollLeft||a.body.scrollLeft};(function(){baidu.page.getMousePosition=function(){return{x:baidu.page.getScrollLeft()+a.x,y:baidu.page.getScrollTop()+a.y}};var a={x:0,y:0};baidu.event.on(document,"onmousemove",function(b){b=window.event||b;a.x=b.clientX;a.y=b.clientY})})();baidu.event.un=function(c,f,b){c=baidu.dom._g(c);f=f.replace(/^on/i,"").toLowerCase();var i=baidu.event._listeners,d=i.length,e=!b,h,g,a;while(d--){h=i[d];if(h[1]===f&&h[0]===c&&(e||h[2]===b)){g=h[4];a=h[3];if(c.removeEventListener){c.removeEventListener(g,a,false)}else{if(c.detachEvent){c.detachEvent("on"+g,a)}}i.splice(d,1)}}return c};baidu.un=baidu.event.un;baidu.event.preventDefault=function(a){if(a.preventDefault){a.preventDefault()}else{a.returnValue=false}};baidu.lang.isFunction=function(a){return"[object Function]"==Object.prototype.toString.call(a)};baidu.lang.isObject=function(a){return"function"==typeof a||!!(a&&"object"==typeof a)};baidu.isObject=baidu.lang.isObject;(function(){var i,h,e,d,c,f,l,a,k,m;baidu.dom.drag=function(o,n){if(!(i=baidu.dom.g(o))){return false}h=baidu.object.extend({autoStop:true,capture:true,interval:16},n);a=f=parseInt(baidu.dom.getStyle(i,"left"))||0;k=l=parseInt(baidu.dom.getStyle(i,"top"))||0;setTimeout(function(){var p=baidu.page.getMousePosition();e=h.mouseEvent?(baidu.page.getScrollLeft()+h.mouseEvent.clientX):p.x;d=h.mouseEvent?(baidu.page.getScrollTop()+h.mouseEvent.clientY):p.y;clearInterval(c);c=setInterval(b,h.interval)},1);h.autoStop&&baidu.event.on(document,"mouseup",j);baidu.event.on(document,"selectstart",g);if(h.capture&&i.setCapture){i.setCapture()}else{if(h.capture&&window.captureEvents){window.captureEvents(Event.MOUSEMOVE|Event.MOUSEUP)}}m=document.body.style.MozUserSelect;document.body.style.MozUserSelect="none";baidu.lang.isFunction(h.ondragstart)&&h.ondragstart(i,h);return{stop:j,dispose:j,update:function(p){baidu.object.extend(h,p)}}};function j(){clearInterval(c);if(h.capture&&i.releaseCapture){i.releaseCapture()}else{if(h.capture&&window.captureEvents){window.captureEvents(Event.MOUSEMOVE|Event.MOUSEUP)}}document.body.style.MozUserSelect=m;baidu.event.un(document,"selectstart",g);h.autoStop&&baidu.event.un(document,"mouseup",j);baidu.lang.isFunction(h.ondragend)&&h.ondragend(i,h,{left:a,top:k})}function b(r){var o=h.range||[],n=baidu.page.getMousePosition(),p=f+n.x-e,q=l+n.y-d;if(baidu.lang.isObject(o)&&o.length==4){p=Math.max(o[3],p);p=Math.min(o[1]-i.offsetWidth,p);q=Math.max(o[0],q);q=Math.min(o[2]-i.offsetHeight,q)}i.style.left=p+"px";i.style.top=q+"px";a=p;k=q;baidu.lang.isFunction(h.ondrag)&&h.ondrag(i,h,{left:a,top:k})}function g(n){return baidu.event.preventDefault(n,false)}})();baidu.dom.setStyle=function(c,b,d){var e=baidu.dom,a;c=e.g(c);b=baidu.string.toCamelCase(b);if(a=e._styleFilter){d=a.filter(b,d,"set")}a=e._styleFixer[b];(a&&a.set)?a.set(c,d,b):(c.style[a||b]=d);return c};baidu.setStyle=baidu.dom.setStyle;baidu.lang.guid=function(){return"TANGRAM$"+baidu.$$._counter++};baidu.$$._counter=baidu.$$._counter||1;baidu.lang.Class=function(){this.guid=baidu.lang.guid();!this.__decontrolled&&(baidu.$$._instances[this.guid]=this)};baidu.$$._instances=baidu.$$._instances||{};baidu.lang.Class.prototype.dispose=function(){delete baidu.$$._instances[this.guid];for(var a in this){typeof this[a]!="function"&&delete this[a]}this.disposed=true};baidu.lang.Class.prototype.toString=function(){return"[object "+(this.__type||this._className||"Object")+"]"};window.baiduInstance=function(a){return baidu.$$._instances[a]};baidu.dom.draggable=function(b,k){k=baidu.object.extend({toggle:function(){return true}},k);k.autoStop=true;b=baidu.dom.g(b);k.handler=k.handler||b;var a,h=["ondragstart","ondrag","ondragend"],c=h.length-1,d,j,f={dispose:function(){j&&j.stop();baidu.event.un(k.handler,"onmousedown",g);baidu.lang.Class.prototype.dispose.call(f)}},e=this;if(a=baidu.dom.ddManager){for(;c>=0;c--){d=h[c];k[d]=(function(i){var l=k[i];return function(){baidu.lang.isFunction(l)&&l.apply(e,arguments);a.dispatchEvent(i,{DOM:b})}})(d)}}if(b){function g(l){var i=k.mouseEvent=window.event||l;k.mouseEvent={clientX:i.clientX,clientY:i.clientY};if(i.button>1||(baidu.lang.isFunction(k.toggle)&&!k.toggle())){return}if(baidu.lang.isFunction(k.onbeforedragstart)){k.onbeforedragstart(b)}j=baidu.dom.drag(b,k);f.stop=j.stop;f.update=j.update;baidu.event.preventDefault(i)}baidu.event.on(k.handler,"onmousedown",g)}return{cancel:function(){f.dispose()}}};baidu.dom.empty=function(a){a=baidu.dom.g(a);while(a.firstChild){a.removeChild(a.firstChild)}return a};baidu.dom.first=function(a){return baidu.dom._matchNode(a,"nextSibling","firstChild")};baidu.dom.last=function(a){return baidu.dom._matchNode(a,"previousSibling","lastChild")};baidu.dom.getAncestorByClass=function(a,b){a=baidu.dom.g(a);b=new RegExp("(^|\\s)"+baidu.string.trim(b)+"(\\s|\x24)");while((a=a.parentNode)&&a.nodeType==1){if(b.test(a.className)){return a}}return null};baidu.dom.getAncestorByTag=function(b,a){b=baidu.dom.g(b);a=a.toUpperCase();while((b=b.parentNode)&&b.nodeType==1){if(b.tagName==a){return b}}return null};baidu.dom.getAttr=function(b,a){b=baidu.dom.g(b);if("style"==a){return b.style.cssText}a=baidu.dom._NAME_ATTRS[a]||a;return b.getAttribute(a)};baidu.getAttr=baidu.dom.getAttr;baidu.dom.getPosition=function(a){a=baidu.dom.g(a);var j=baidu.dom.getDocument(a),d=baidu.browser,g=baidu.dom.getStyle,c=d.isGecko>0&&j.getBoxObjectFor&&g(a,"position")=="absolute"&&(a.style.top===""||a.style.left===""),h={left:0,top:0},f=(d.ie&&!d.isStrict)?j.body:j.documentElement,k,b;if(a==f){return h}if(a.getBoundingClientRect){b=a.getBoundingClientRect();h.left=Math.floor(b.left)+Math.max(j.documentElement.scrollLeft,j.body.scrollLeft);h.top=Math.floor(b.top)+Math.max(j.documentElement.scrollTop,j.body.scrollTop);h.left-=j.documentElement.clientLeft;h.top-=j.documentElement.clientTop;var i=j.body,l=parseInt(g(i,"borderLeftWidth")),e=parseInt(g(i,"borderTopWidth"));if(d.ie&&!d.isStrict){h.left-=isNaN(l)?2:l;h.top-=isNaN(e)?2:e}}else{k=a;do{h.left+=k.offsetLeft;h.top+=k.offsetTop;if(d.isWebkit>0&&g(k,"position")=="fixed"){h.left+=j.body.scrollLeft;h.top+=j.body.scrollTop;break}k=k.offsetParent}while(k&&k!=a);if(d.opera>0||(d.isWebkit>0&&g(a,"position")=="absolute")){h.top-=j.body.offsetTop}k=a.offsetParent;while(k&&k!=j.body){h.left-=k.scrollLeft;if(!d.opera||k.tagName!="TR"){h.top-=k.scrollTop}k=k.offsetParent}}return h};baidu.dom.hasClass=function(c,d){c=baidu.dom.g(c);if(!c||!c.className){return false}var b=baidu.string.trim(d).split(/\s+/),a=b.length;d=c.className.split(/\s+/).join(" ");while(a--){if(!(new RegExp("(^| )"+b[a]+"( |\x24)")).test(d)){return false}}return true};baidu.dom.hide=function(a){a=baidu.dom.g(a);a.style.display="none";return a};baidu.hide=baidu.dom.hide;baidu.dom.insertAfter=function(d,c){var b,a;b=baidu.dom._g;d=b(d);c=b(c);a=c.parentNode;if(a){a.insertBefore(d,c.nextSibling)}return d};baidu.dom.insertBefore=function(d,c){var b,a;b=baidu.dom._g;d=b(d);c=b(c);a=c.parentNode;if(a){a.insertBefore(d,c)}return d};baidu.dom.insertHTML=function(d,a,c){d=baidu.dom.g(d);var b,e;if(d.insertAdjacentHTML&&!baidu.browser.opera){d.insertAdjacentHTML(a,c)}else{b=d.ownerDocument.createRange();a=a.toUpperCase();if(a=="AFTERBEGIN"||a=="BEFOREEND"){b.selectNodeContents(d);b.collapse(a=="AFTERBEGIN")}else{e=a=="BEFOREBEGIN";b[e?"setStartBefore":"setEndAfter"](d);b.collapse(e)}b.insertNode(b.createContextualFragment(c))}return d};baidu.insertHTML=baidu.dom.insertHTML;baidu.dom.next=function(a){return baidu.dom._matchNode(a,"nextSibling","nextSibling")};baidu.dom.prev=function(a){return baidu.dom._matchNode(a,"previousSibling","previousSibling")};baidu.string.escapeReg=function(a){return String(a).replace(new RegExp("([.*+?^=!:\x24{}()|[\\]/\\\\])","g"),"\\\x241")};baidu.dom.q=function(h,e,b){var j=[],d=baidu.string.trim,g,f,a,c;if(!(h=d(h))){return j}if("undefined"==typeof e){e=document}else{e=baidu.dom.g(e);if(!e){return j}}b&&(b=d(b).toUpperCase());if(e.getElementsByClassName){a=e.getElementsByClassName(h);g=a.length;for(f=0;f<g;f++){c=a[f];if(b&&c.tagName!=b){continue}j[j.length]=c}}else{h=new RegExp("(^|\\s)"+baidu.string.escapeReg(h)+"(\\s|\x24)");a=b?e.getElementsByTagName(b):(e.all||e.getElementsByTagName("*"));g=a.length;for(f=0;f<g;f++){c=a[f];h.test(c.className)&&(j[j.length]=c)}}return j};baidu.q=baidu.Q=baidu.dom.q;
/*
 * Sizzle CSS Selector Engine
 *  Copyright 2011, The Dojo Foundation
 *  Released under the MIT, BSD, and GPL Licenses.
 *  More information: http://sizzlejs.com/
 */
(function(){var n=/((?:\((?:\([^()]+\)|[^()]+)+\)|\[(?:\[[^\[\]]*\]|['"][^'"]*['"]|[^\[\]'"]+)+\]|\\.|[^ >+~,(\[\\]+)+|[>+~])(\s*,\s*)?((?:.|\r|\n)*)/g,i="sizcache"+(Math.random()+"").replace(".",""),o=0,r=Object.prototype.toString,h=false,g=true,q=/\\/g,u=/\r\n/g,w=/\W/;[0,0].sort(function(){g=false;return 0});var d=function(B,e,E,F){E=E||[];e=e||document;var H=e;if(e.nodeType!==1&&e.nodeType!==9){return[]}if(!B||typeof B!=="string"){return E}var y,J,M,x,I,L,K,D,A=true,z=d.isXML(e),C=[],G=B;do{n.exec("");y=n.exec(G);if(y){G=y[3];C.push(y[1]);if(y[2]){x=y[3];break}}}while(y);if(C.length>1&&j.exec(B)){if(C.length===2&&k.relative[C[0]]){J=s(C[0]+C[1],e,F)}else{J=k.relative[C[0]]?[e]:d(C.shift(),e);while(C.length){B=C.shift();if(k.relative[B]){B+=C.shift()}J=s(B,J,F)}}}else{if(!F&&C.length>1&&e.nodeType===9&&!z&&k.match.ID.test(C[0])&&!k.match.ID.test(C[C.length-1])){I=d.find(C.shift(),e,z);e=I.expr?d.filter(I.expr,I.set)[0]:I.set[0]}if(e){I=F?{expr:C.pop(),set:l(F)}:d.find(C.pop(),C.length===1&&(C[0]==="~"||C[0]==="+")&&e.parentNode?e.parentNode:e,z);J=I.expr?d.filter(I.expr,I.set):I.set;if(C.length>0){M=l(J)}else{A=false}while(C.length){L=C.pop();K=L;if(!k.relative[L]){L=""}else{K=C.pop()}if(K==null){K=e}k.relative[L](M,K,z)}}else{M=C=[]}}if(!M){M=J}if(!M){d.error(L||B)}if(r.call(M)==="[object Array]"){if(!A){E.push.apply(E,M)}else{if(e&&e.nodeType===1){for(D=0;M[D]!=null;D++){if(M[D]&&(M[D]===true||M[D].nodeType===1&&d.contains(e,M[D]))){E.push(J[D])}}}else{for(D=0;M[D]!=null;D++){if(M[D]&&M[D].nodeType===1){E.push(J[D])}}}}}else{l(M,E)}if(x){d(x,H,E,F);d.uniqueSort(E)}return E};d.uniqueSort=function(x){if(p){h=g;x.sort(p);if(h){for(var e=1;e<x.length;e++){if(x[e]===x[e-1]){x.splice(e--,1)}}}}return x};d.matches=function(e,x){return d(e,null,null,x)};d.matchesSelector=function(e,x){return d(x,null,null,[e]).length>0};d.find=function(D,e,E){var C,y,A,z,B,x;if(!D){return[]}for(y=0,A=k.order.length;y<A;y++){B=k.order[y];if((z=k.leftMatch[B].exec(D))){x=z[1];z.splice(1,1);if(x.substr(x.length-1)!=="\\"){z[1]=(z[1]||"").replace(q,"");C=k.find[B](z,e,E);if(C!=null){D=D.replace(k.match[B],"");break}}}}if(!C){C=typeof e.getElementsByTagName!=="undefined"?e.getElementsByTagName("*"):[]}return{set:C,expr:D}};d.filter=function(H,G,K,A){var C,e,F,M,J,x,z,B,I,y=H,L=[],E=G,D=G&&G[0]&&d.isXML(G[0]);while(H&&G.length){for(F in k.filter){if((C=k.leftMatch[F].exec(H))!=null&&C[2]){x=k.filter[F];z=C[1];e=false;C.splice(1,1);if(z.substr(z.length-1)==="\\"){continue}if(E===L){L=[]}if(k.preFilter[F]){C=k.preFilter[F](C,E,K,L,A,D);if(!C){e=M=true}else{if(C===true){continue}}}if(C){for(B=0;(J=E[B])!=null;B++){if(J){M=x(J,C,B,E);I=A^M;if(K&&M!=null){if(I){e=true}else{E[B]=false}}else{if(I){L.push(J);e=true}}}}}if(M!==undefined){if(!K){E=L}H=H.replace(k.match[F],"");if(!e){return[]}break}}}if(H===y){if(e==null){d.error(H)}else{break}}y=H}return E};d.error=function(e){throw"Syntax error, unrecognized expression: "+e};var b=d.getText=function(A){var y,z,e=A.nodeType,x="";if(e){if(e===1){if(typeof A.textContent==="string"){return A.textContent}else{if(typeof A.innerText==="string"){return A.innerText.replace(u,"")}else{for(A=A.firstChild;A;A=A.nextSibling){x+=b(A)}}}}else{if(e===3||e===4){return A.nodeValue}}}else{for(y=0;(z=A[y]);y++){if(z.nodeType!==8){x+=b(z)}}}return x};var k=d.selectors={order:["ID","NAME","TAG"],match:{ID:/#((?:[\w\u00c0-\uFFFF\-]|\\.)+)/,CLASS:/\.((?:[\w\u00c0-\uFFFF\-]|\\.)+)/,NAME:/\[name=['"]*((?:[\w\u00c0-\uFFFF\-]|\\.)+)['"]*\]/,ATTR:/\[\s*((?:[\w\u00c0-\uFFFF\-]|\\.)+)\s*(?:(\S?=)\s*(?:(['"])(.*?)\3|(#?(?:[\w\u00c0-\uFFFF\-]|\\.)*)|)|)\s*\]/,TAG:/^((?:[\w\u00c0-\uFFFF\*\-]|\\.)+)/,CHILD:/:(only|nth|last|first)-child(?:\(\s*(even|odd|(?:[+\-]?\d+|(?:[+\-]?\d*)?n\s*(?:[+\-]\s*\d+)?))\s*\))?/,POS:/:(nth|eq|gt|lt|first|last|even|odd)(?:\((\d*)\))?(?=[^\-]|$)/,PSEUDO:/:((?:[\w\u00c0-\uFFFF\-]|\\.)+)(?:\((['"]?)((?:\([^\)]+\)|[^\(\)]*)+)\2\))?/},leftMatch:{},attrMap:{"class":"className","for":"htmlFor"},attrHandle:{href:function(e){return e.getAttribute("href")},type:function(e){return e.getAttribute("type")}},relative:{"+":function(C,x){var z=typeof x==="string",B=z&&!w.test(x),D=z&&!B;if(B){x=x.toLowerCase()}for(var y=0,e=C.length,A;y<e;y++){if((A=C[y])){while((A=A.previousSibling)&&A.nodeType!==1){}C[y]=D||A&&A.nodeName.toLowerCase()===x?A||false:A===x}}if(D){d.filter(x,C,true)}},">":function(C,x){var B,A=typeof x==="string",y=0,e=C.length;if(A&&!w.test(x)){x=x.toLowerCase();for(;y<e;y++){B=C[y];if(B){var z=B.parentNode;C[y]=z.nodeName.toLowerCase()===x?z:false}}}else{for(;y<e;y++){B=C[y];if(B){C[y]=A?B.parentNode:B.parentNode===x}}if(A){d.filter(x,C,true)}}},"":function(z,x,B){var A,y=o++,e=t;if(typeof x==="string"&&!w.test(x)){x=x.toLowerCase();A=x;e=a}e("parentNode",x,y,z,A,B)},"~":function(z,x,B){var A,y=o++,e=t;if(typeof x==="string"&&!w.test(x)){x=x.toLowerCase();A=x;e=a}e("previousSibling",x,y,z,A,B)}},find:{ID:function(x,y,z){if(typeof y.getElementById!=="undefined"&&!z){var e=y.getElementById(x[1]);return e&&e.parentNode?[e]:[]}},NAME:function(y,B){if(typeof B.getElementsByName!=="undefined"){var x=[],A=B.getElementsByName(y[1]);for(var z=0,e=A.length;z<e;z++){if(A[z].getAttribute("name")===y[1]){x.push(A[z])}}return x.length===0?null:x}},TAG:function(e,x){if(typeof x.getElementsByTagName!=="undefined"){return x.getElementsByTagName(e[1])}}},preFilter:{CLASS:function(z,x,y,e,C,D){z=" "+z[1].replace(q,"")+" ";if(D){return z}for(var A=0,B;(B=x[A])!=null;A++){if(B){if(C^(B.className&&(" "+B.className+" ").replace(/[\t\n\r]/g," ").indexOf(z)>=0)){if(!y){e.push(B)}}else{if(y){x[A]=false}}}}return false},ID:function(e){return e[1].replace(q,"")},TAG:function(x,e){return x[1].replace(q,"").toLowerCase()},CHILD:function(e){if(e[1]==="nth"){if(!e[2]){d.error(e[0])}e[2]=e[2].replace(/^\+|\s*/g,"");var x=/(-?)(\d*)(?:n([+\-]?\d*))?/.exec(e[2]==="even"&&"2n"||e[2]==="odd"&&"2n+1"||!/\D/.test(e[2])&&"0n+"+e[2]||e[2]);e[2]=(x[1]+(x[2]||1))-0;e[3]=x[3]-0}else{if(e[2]){d.error(e[0])}}e[0]=o++;return e},ATTR:function(A,x,y,e,B,C){var z=A[1]=A[1].replace(q,"");if(!C&&k.attrMap[z]){A[1]=k.attrMap[z]}A[4]=(A[4]||A[5]||"").replace(q,"");if(A[2]==="~="){A[4]=" "+A[4]+" "}return A},PSEUDO:function(A,x,y,e,B){if(A[1]==="not"){if((n.exec(A[3])||"").length>1||/^\w/.test(A[3])){A[3]=d(A[3],null,null,x)}else{var z=d.filter(A[3],x,y,true^B);if(!y){e.push.apply(e,z)}return false}}else{if(k.match.POS.test(A[0])||k.match.CHILD.test(A[0])){return true}}return A},POS:function(e){e.unshift(true);return e}},filters:{enabled:function(e){return e.disabled===false&&e.type!=="hidden"},disabled:function(e){return e.disabled===true},checked:function(e){return e.checked===true},selected:function(e){if(e.parentNode){e.parentNode.selectedIndex}return e.selected===true},parent:function(e){return !!e.firstChild},empty:function(e){return !e.firstChild},has:function(y,x,e){return !!d(e[3],y).length},header:function(e){return(/h\d/i).test(e.nodeName)},text:function(y){var e=y.getAttribute("type"),x=y.type;return y.nodeName.toLowerCase()==="input"&&"text"===x&&(e===x||e===null)},radio:function(e){return e.nodeName.toLowerCase()==="input"&&"radio"===e.type},checkbox:function(e){return e.nodeName.toLowerCase()==="input"&&"checkbox"===e.type},file:function(e){return e.nodeName.toLowerCase()==="input"&&"file"===e.type},password:function(e){return e.nodeName.toLowerCase()==="input"&&"password"===e.type},submit:function(x){var e=x.nodeName.toLowerCase();return(e==="input"||e==="button")&&"submit"===x.type},image:function(e){return e.nodeName.toLowerCase()==="input"&&"image"===e.type},reset:function(x){var e=x.nodeName.toLowerCase();return(e==="input"||e==="button")&&"reset"===x.type},button:function(x){var e=x.nodeName.toLowerCase();return e==="input"&&"button"===x.type||e==="button"},input:function(e){return(/input|select|textarea|button/i).test(e.nodeName)},focus:function(e){return e===e.ownerDocument.activeElement}},setFilters:{first:function(x,e){return e===0},last:function(y,x,e,z){return x===z.length-1},even:function(x,e){return e%2===0},odd:function(x,e){return e%2===1},lt:function(y,x,e){return x<e[3]-0},gt:function(y,x,e){return x>e[3]-0},nth:function(y,x,e){return e[3]-0===x},eq:function(y,x,e){return e[3]-0===x}},filter:{PSEUDO:function(y,D,C,E){var e=D[1],x=k.filters[e];if(x){return x(y,C,D,E)}else{if(e==="contains"){return(y.textContent||y.innerText||b([y])||"").indexOf(D[3])>=0}else{if(e==="not"){var z=D[3];for(var B=0,A=z.length;B<A;B++){if(z[B]===y){return false}}return true}else{d.error(e)}}}},CHILD:function(y,A){var z,G,C,F,e,B,E,D=A[1],x=y;switch(D){case"only":case"first":while((x=x.previousSibling)){if(x.nodeType===1){return false}}if(D==="first"){return true}x=y;case"last":while((x=x.nextSibling)){if(x.nodeType===1){return false}}return true;case"nth":z=A[2];G=A[3];if(z===1&&G===0){return true}C=A[0];F=y.parentNode;if(F&&(F[i]!==C||!y.nodeIndex)){B=0;for(x=F.firstChild;x;x=x.nextSibling){if(x.nodeType===1){x.nodeIndex=++B}}F[i]=C}E=y.nodeIndex-G;if(z===0){return E===0}else{return(E%z===0&&E/z>=0)}}},ID:function(x,e){return x.nodeType===1&&x.getAttribute("id")===e},TAG:function(x,e){return(e==="*"&&x.nodeType===1)||!!x.nodeName&&x.nodeName.toLowerCase()===e},CLASS:function(x,e){return(" "+(x.className||x.getAttribute("class"))+" ").indexOf(e)>-1},ATTR:function(B,z){var y=z[1],e=d.attr?d.attr(B,y):k.attrHandle[y]?k.attrHandle[y](B):B[y]!=null?B[y]:B.getAttribute(y),C=e+"",A=z[2],x=z[4];return e==null?A==="!=":!A&&d.attr?e!=null:A==="="?C===x:A==="*="?C.indexOf(x)>=0:A==="~="?(" "+C+" ").indexOf(x)>=0:!x?C&&e!==false:A==="!="?C!==x:A==="^="?C.indexOf(x)===0:A==="$="?C.substr(C.length-x.length)===x:A==="|="?C===x||C.substr(0,x.length+1)===x+"-":false},POS:function(A,x,y,B){var e=x[2],z=k.setFilters[e];if(z){return z(A,y,x,B)}}}};var j=k.match.POS,c=function(x,e){return"\\"+(e-0+1)};for(var f in k.match){k.match[f]=new RegExp(k.match[f].source+(/(?![^\[]*\])(?![^\(]*\))/.source));k.leftMatch[f]=new RegExp(/(^(?:.|\r|\n)*?)/.source+k.match[f].source.replace(/\\(\d+)/g,c))}var l=function(x,e){x=Array.prototype.slice.call(x,0);if(e){e.push.apply(e,x);return e}return x};try{Array.prototype.slice.call(document.documentElement.childNodes,0)[0].nodeType}catch(v){l=function(A,z){var y=0,x=z||[];if(r.call(A)==="[object Array]"){Array.prototype.push.apply(x,A)}else{if(typeof A.length==="number"){for(var e=A.length;y<e;y++){x.push(A[y])}}else{for(;A[y];y++){x.push(A[y])}}}return x}}var p,m;if(document.documentElement.compareDocumentPosition){p=function(x,e){if(x===e){h=true;return 0}if(!x.compareDocumentPosition||!e.compareDocumentPosition){return x.compareDocumentPosition?-1:1}return x.compareDocumentPosition(e)&4?-1:1}}else{p=function(E,D){if(E===D){h=true;return 0}else{if(E.sourceIndex&&D.sourceIndex){return E.sourceIndex-D.sourceIndex}}var B,x,y=[],e=[],A=E.parentNode,C=D.parentNode,F=A;if(A===C){return m(E,D)}else{if(!A){return -1}else{if(!C){return 1}}}while(F){y.unshift(F);F=F.parentNode}F=C;while(F){e.unshift(F);F=F.parentNode}B=y.length;x=e.length;for(var z=0;z<B&&z<x;z++){if(y[z]!==e[z]){return m(y[z],e[z])}}return z===B?m(E,e[z],-1):m(y[z],D,1)};m=function(x,e,y){if(x===e){return y}var z=x.nextSibling;while(z){if(z===e){return -1}z=z.nextSibling}return 1}}(function(){var x=document.createElement("div"),y="script"+(new Date()).getTime(),e=document.documentElement;x.innerHTML="<a name='"+y+"'/>";e.insertBefore(x,e.firstChild);if(document.getElementById(y)){k.find.ID=function(A,B,C){if(typeof B.getElementById!=="undefined"&&!C){var z=B.getElementById(A[1]);return z?z.id===A[1]||typeof z.getAttributeNode!=="undefined"&&z.getAttributeNode("id").nodeValue===A[1]?[z]:undefined:[]}};k.filter.ID=function(B,z){var A=typeof B.getAttributeNode!=="undefined"&&B.getAttributeNode("id");return B.nodeType===1&&A&&A.nodeValue===z}}e.removeChild(x);e=x=null})();(function(){var e=document.createElement("div");e.appendChild(document.createComment(""));if(e.getElementsByTagName("*").length>0){k.find.TAG=function(x,B){var A=B.getElementsByTagName(x[1]);if(x[1]==="*"){var z=[];for(var y=0;A[y];y++){if(A[y].nodeType===1){z.push(A[y])}}A=z}return A}}e.innerHTML="<a href='#'></a>";if(e.firstChild&&typeof e.firstChild.getAttribute!=="undefined"&&e.firstChild.getAttribute("href")!=="#"){k.attrHandle.href=function(x){return x.getAttribute("href",2)}}e=null})();if(document.querySelectorAll){(function(){var e=d,z=document.createElement("div"),y="__sizzle__";z.innerHTML="<p class='TEST'></p>";if(z.querySelectorAll&&z.querySelectorAll(".TEST").length===0){return}d=function(K,B,F,J){B=B||document;if(!J&&!d.isXML(B)){var I=/^(\w+$)|^\.([\w\-]+$)|^#([\w\-]+$)/.exec(K);if(I&&(B.nodeType===1||B.nodeType===9)){if(I[1]){return l(B.getElementsByTagName(K),F)}else{if(I[2]&&k.find.CLASS&&B.getElementsByClassName){return l(B.getElementsByClassName(I[2]),F)}}}if(B.nodeType===9){if(K==="body"&&B.body){return l([B.body],F)}else{if(I&&I[3]){var E=B.getElementById(I[3]);if(E&&E.parentNode){if(E.id===I[3]){return l([E],F)}}else{return l([],F)}}}try{return l(B.querySelectorAll(K),F)}catch(G){}}else{if(B.nodeType===1&&B.nodeName.toLowerCase()!=="object"){var C=B,D=B.getAttribute("id"),A=D||y,M=B.parentNode,L=/^\s*[+~]/.test(K);if(!D){B.setAttribute("id",A)}else{A=A.replace(/'/g,"\\$&")}if(L&&M){B=B.parentNode}try{if(!L||M){return l(B.querySelectorAll("[id='"+A+"'] "+K),F)}}catch(H){}finally{if(!D){C.removeAttribute("id")}}}}}return e(K,B,F,J)};for(var x in e){d[x]=e[x]}z=null})()}(function(){var e=document.documentElement,y=e.matchesSelector||e.mozMatchesSelector||e.webkitMatchesSelector||e.msMatchesSelector;if(y){var A=!y.call(document.createElement("div"),"div"),x=false;try{y.call(document.documentElement,"[test!='']:sizzle")}catch(z){x=true}d.matchesSelector=function(C,E){E=E.replace(/\=\s*([^'"\]]*)\s*\]/g,"='$1']");if(!d.isXML(C)){try{if(x||!k.match.PSEUDO.test(E)&&!/!=/.test(E)){var B=y.call(C,E);if(B||!A||C.document&&C.document.nodeType!==11){return B}}}catch(D){}}return d(E,null,null,[C]).length>0}}})();(function(){var e=document.createElement("div");e.innerHTML="<div class='test e'></div><div class='test'></div>";if(!e.getElementsByClassName||e.getElementsByClassName("e").length===0){return}e.lastChild.className="e";if(e.getElementsByClassName("e").length===1){return}k.order.splice(1,0,"CLASS");k.find.CLASS=function(x,y,z){if(typeof y.getElementsByClassName!=="undefined"&&!z){return y.getElementsByClassName(x[1])}};e=null})();function a(x,C,B,F,D,E){for(var z=0,y=F.length;z<y;z++){var e=F[z];if(e){var A=false;e=e[x];while(e){if(e[i]===B){A=F[e.sizset];break}if(e.nodeType===1&&!E){e[i]=B;e.sizset=z}if(e.nodeName.toLowerCase()===C){A=e;break}e=e[x]}F[z]=A}}}function t(x,C,B,F,D,E){for(var z=0,y=F.length;z<y;z++){var e=F[z];if(e){var A=false;e=e[x];while(e){if(e[i]===B){A=F[e.sizset];break}if(e.nodeType===1){if(!E){e[i]=B;e.sizset=z}if(typeof C!=="string"){if(e===C){A=true;break}}else{if(d.filter(C,[e]).length>0){A=e;break}}}e=e[x]}F[z]=A}}}if(document.documentElement.contains){d.contains=function(x,e){return x!==e&&(x.contains?x.contains(e):true)}}else{if(document.documentElement.compareDocumentPosition){d.contains=function(x,e){return !!(x.compareDocumentPosition(e)&16)}}else{d.contains=function(){return false}}}d.isXML=function(e){var x=(e?e.ownerDocument||e:0).documentElement;return x?x.nodeName!=="HTML":false};var s=function(y,e,C){var B,D=[],A="",E=e.nodeType?[e]:e;while((B=k.match.PSEUDO.exec(y))){A+=B[0];y=y.replace(k.match.PSEUDO,"")}y=k.relative[y]?y+"*":y;for(var z=0,x=E.length;z<x;z++){d(y,E[z],D,C)}return d.filter(A,D)};baidu.dom.query=d})();(function(){var a=baidu.dom.ready=function(){var g=false,f=[],c;if(document.addEventListener){c=function(){document.removeEventListener("DOMContentLoaded",c,false);d()}}else{if(document.attachEvent){c=function(){if(document.readyState==="complete"){document.detachEvent("onreadystatechange",c);d()}}}}function d(){if(!d.isReady){d.isReady=true;for(var k=0,h=f.length;k<h;k++){f[k]()}}}function b(){try{document.documentElement.doScroll("left")}catch(h){setTimeout(b,1);return}d()}function e(){if(g){return}g=true;if(document.readyState==="complete"){d.isReady=true}else{if(document.addEventListener){document.addEventListener("DOMContentLoaded",c,false);window.addEventListener("load",d,false)}else{if(document.attachEvent){document.attachEvent("onreadystatechange",c);window.attachEvent("onload",d);var h=false;try{h=window.frameElement==null}catch(i){}if(document.documentElement.doScroll&&h){b()}}}}}e();return function(h){d.isReady?h():f.push(h)}}();a.isReady=false})();baidu.dom.remove=function(a){a=baidu.dom._g(a);var b=a.parentNode;b&&b.removeChild(a)};baidu.dom.removeClass=function(f,g){f=baidu.dom.g(f);var d=f.className.split(/\s+/),h=g.split(/\s+/),b,a=h.length,c,e=0;for(;e<a;++e){for(c=0,b=d.length;c<b;++c){if(d[c]==h[e]){d.splice(c,1);break}}}f.className=d.join(" ");return f};baidu.removeClass=baidu.dom.removeClass;baidu.dom.removeStyle=function(){var b=document.createElement("DIV"),a,c=baidu.dom._g;if(b.style.removeProperty){a=function(e,d){e=c(e);e.style.removeProperty(d);return e}}else{if(b.style.removeAttribute){a=function(e,d){e=c(e);e.style.removeAttribute(baidu.string.toCamelCase(d));return e}}}b=null;return a}();baidu.dom.setStyles=function(b,c){b=baidu.dom.g(b);for(var a in c){baidu.dom.setStyle(b,a,c[a])}return b};baidu.setStyles=baidu.dom.setStyles;baidu.dom.show=function(a){a=baidu.dom.g(a);a.style.display="";return a};baidu.show=baidu.dom.show;baidu.dom.toggle=function(a){a=baidu.dom.g(a);a.style.display=a.style.display=="none"?"":"none";return a};baidu.dom.toggleClass=function(a,b){if(baidu.dom.hasClass(a,b)){baidu.dom.removeClass(a,b)}else{baidu.dom.addClass(a,b)}};baidu.event.EventArg=function(c,e){e=e||window;c=c||e.event;var d=e.document;this.target=(c.target)||c.srcElement;this.keyCode=c.which||c.keyCode;for(var a in c){var b=c[a];if("function"!=typeof b){this[a]=b}}if(!this.pageX&&this.pageX!==0){this.pageX=(c.clientX||0)+(d.documentElement.scrollLeft||d.body.scrollLeft);this.pageY=(c.clientY||0)+(d.documentElement.scrollTop||d.body.scrollTop)}this._event=c};baidu.event.EventArg.prototype.preventDefault=function(){if(this._event.preventDefault){this._event.preventDefault()}else{this._event.returnValue=false}return this};baidu.event.EventArg.prototype.stopPropagation=function(){if(this._event.stopPropagation){this._event.stopPropagation()}else{this._event.cancelBubble=true}return this};baidu.event.EventArg.prototype.stop=function(){return this.stopPropagation().preventDefault()};baidu.event.get=function(a,b){return new baidu.event.EventArg(a,b)};baidu.event.getPageX=function(b){var a=b.pageX,c=document;if(!a&&a!==0){a=(b.clientX||0)+(c.documentElement.scrollLeft||c.body.scrollLeft)}return a};baidu.event.getPageY=function(b){var a=b.pageY,c=document;if(!a&&a!==0){a=(b.clientY||0)+(c.documentElement.scrollTop||c.body.scrollTop)}return a};baidu.event.getTarget=function(a){return a.target||a.srcElement};baidu.event.stopPropagation=function(a){if(a.stopPropagation){a.stopPropagation()}else{a.cancelBubble=true}};baidu.fn.bind=function(b,a){var c=arguments.length>2?[].slice.call(arguments,2):null;return function(){var e=baidu.lang.isString(b)?a[b]:b,d=(c)?c.concat([].slice.call(arguments,0)):arguments;return e.apply(a||e,d)}};baidu.json=baidu.json||{};baidu.json.parse=function(a){return(new Function("return ("+a+")"))()};baidu.json.stringify=(function(){var b={"\b":"\\b","\t":"\\t","\n":"\\n","\f":"\\f","\r":"\\r",'"':'\\"',"\\":"\\\\"};function a(f){if(/["\\\x00-\x1f]/.test(f)){f=f.replace(/["\\\x00-\x1f]/g,function(g){var h=b[g];if(h){return h}h=g.charCodeAt();return"\\u00"+Math.floor(h/16).toString(16)+(h%16).toString(16)})}return'"'+f+'"'}function d(m){var g=["["],h=m.length,f,j,k;for(j=0;j<h;j++){k=m[j];switch(typeof k){case"undefined":case"function":case"unknown":break;default:if(f){g.push(",")}g.push(baidu.json.stringify(k));f=1}}g.push("]");return g.join("")}function c(f){return f<10?"0"+f:f}function e(f){return'"'+f.getFullYear()+"-"+c(f.getMonth()+1)+"-"+c(f.getDate())+"T"+c(f.getHours())+":"+c(f.getMinutes())+":"+c(f.getSeconds())+'"'}return function(k){switch(typeof k){case"undefined":return"undefined";case"number":return isFinite(k)?String(k):"null";case"string":return a(k);case"boolean":return String(k);default:if(k===null){return"null"}else{if(k instanceof Array){return d(k)}else{if(k instanceof Date){return e(k)}else{var g=["{"],j=baidu.json.stringify,f,i;for(var h in k){if(Object.prototype.hasOwnProperty.call(k,h)){i=k[h];switch(typeof i){case"undefined":case"unknown":case"function":break;default:if(f){g.push(",")}f=1;g.push(j(h)+":"+j(i))}}}g.push("}");return g.join("")}}}}}})();baidu.lang.Event=function(a,b){this.type=a;this.returnValue=true;this.target=b||null;this.currentTarget=null};baidu.lang.Class.prototype.fire=baidu.lang.Class.prototype.dispatchEvent=function(e,a){baidu.lang.isString(e)&&(e=new baidu.lang.Event(e));!this.__listeners&&(this.__listeners={});a=a||{};for(var c in a){e[c]=a[c]}var c,g,d=this,b=d.__listeners,f=e.type;e.target=e.target||(e.currentTarget=d);f.indexOf("on")&&(f="on"+f);typeof d[f]=="function"&&d[f].apply(d,arguments);if(typeof b[f]=="object"){for(c=0,g=b[f].length;c<g;c++){b[f][c]&&b[f][c].apply(d,arguments)}}return e.returnValue};baidu.lang.Class.prototype.on=baidu.lang.Class.prototype.addEventListener=function(e,d,c){if(typeof d!="function"){return}!this.__listeners&&(this.__listeners={});var b,a=this.__listeners;e.indexOf("on")&&(e="on"+e);typeof a[e]!="object"&&(a[e]=[]);for(b=a[e].length-1;b>=0;b--){if(a[e][b]===d){return d}}a[e].push(d);c&&typeof c=="string"&&(a[e][c]=d);return d};window[baidu.guid]._instances=window[baidu.guid]._instances||{};baidu.lang.inherits=function(g,e,d){var c,f,a=g.prototype,b=new Function();b.prototype=e.prototype;f=g.prototype=new b();for(c in a){f[c]=a[c]}g.prototype.constructor=g;g.superClass=e.prototype;typeof d=="string"&&(f.__type=d);g.extend=function(j){for(var h in j){f[h]=j[h]}return g};return g};baidu.inherits=baidu.lang.inherits;baidu.lang.instance=function(a){return window[baidu.guid]._instances[a]||null};baidu.lang.isArray=function(a){return"[object Array]"==Object.prototype.toString.call(a)};baidu.lang.isElement=function(a){return !!(a&&a.nodeName&&a.nodeType==1)};baidu.lang.isNumber=function(a){return"[object Number]"==Object.prototype.toString.call(a)&&isFinite(a)};baidu.lang.module=function(name,module,owner){var packages=name.split("."),len=packages.length-1,packageName,i=0;if(!owner){try{if(!(new RegExp("^[a-zA-Z_\x24][a-zA-Z0-9_\x24]*\x24")).test(packages[0])){throw""}owner=eval(packages[0]);i=1}catch(e){owner=window}}for(;i<len;i++){packageName=packages[i];if(!owner[packageName]){owner[packageName]={}}owner=owner[packageName]}if(!owner[packages[len]]){owner[packages[len]]=module}};baidu.lang.toArray=function(b){if(b===null||b===undefined){return[]}if(baidu.lang.isArray(b)){return b}if(typeof b.length!=="number"||typeof b==="string"||baidu.lang.isFunction(b)){return[b]}if(b.item){var a=b.length,c=new Array(a);while(a--){c[a]=b[a]}return c}return[].slice.call(b)};baidu.object.isPlain=function(c){var b=Object.prototype.hasOwnProperty,a;if(!c||Object.prototype.toString.call(c)!=="[object Object]"||!("isPrototypeOf" in c)){return false}if(c.constructor&&!b.call(c,"constructor")&&!b.call(c.constructor.prototype,"isPrototypeOf")){return false}for(a in c){}return a===undefined||b.call(c,a)};baidu.object.clone=function(e){var b=e,c,a;if(!e||e instanceof Number||e instanceof String||e instanceof Boolean){return b}else{if(baidu.lang.isArray(e)){b=[];var d=0;for(c=0,a=e.length;c<a;c++){b[d++]=baidu.object.clone(e[c])}}else{if(baidu.object.isPlain(e)){b={};for(c in e){if(e.hasOwnProperty(c)){b[c]=baidu.object.clone(e[c])}}}}}return b};baidu.page.getHeight=function(){var d=document,a=d.body,c=d.documentElement,b=d.compatMode=="BackCompat"?a:d.documentElement;return Math.max(c.scrollHeight,a.scrollHeight,b.clientHeight)};baidu.page.getViewHeight=function(){var b=document,a=b.compatMode=="BackCompat"?b.body:b.documentElement;return a.clientHeight};baidu.page.getViewWidth=function(){var b=document,a=b.compatMode=="BackCompat"?b.body:b.documentElement;return a.clientWidth};baidu.page.getWidth=function(){var d=document,a=d.body,c=d.documentElement,b=d.compatMode=="BackCompat"?a:d.documentElement;return Math.max(c.scrollWidth,a.scrollWidth,b.clientWidth)};baidu.sio=baidu.sio||{};baidu.sio._removeScriptTag=function(b){if(b.clearAttributes){b.clearAttributes()}else{for(var a in b){if(b.hasOwnProperty(a)){delete b[a]}}}if(b&&b.parentNode){b.parentNode.removeChild(b)}b=null};baidu.sio._createScriptTag=function(b,a,c){b.setAttribute("type","text/javascript");c&&b.setAttribute("charset",c);b.setAttribute("src",a);document.getElementsByTagName("head")[0].appendChild(b)};baidu.sio.callByBrowser=function(a,g,i){var d=document.createElement("SCRIPT"),e=0,j=i||{},c=j.charset,h=g||function(){},f=j.timeOut||0,b;d.onload=d.onreadystatechange=function(){if(e){return}var k=d.readyState;if("undefined"==typeof k||k=="loaded"||k=="complete"){e=1;try{h();clearTimeout(b)}finally{d.onload=d.onreadystatechange=null;baidu.sio._removeScriptTag(d)}}};if(f){b=setTimeout(function(){d.onload=d.onreadystatechange=null;baidu.sio._removeScriptTag(d);j.onfailure&&j.onfailure()},f)}baidu.sio._createScriptTag(d,a,c)};baidu.sio.callByServer=function(a,m,n){var i=document.createElement("SCRIPT"),h="bd__cbs__",k,e,o=n||{},d=o.charset,f=o.queryField||"callback",l=o.timeOut||0,b,c=new RegExp("(\\?|&)"+f+"=([^&]*)"),g;if(baidu.lang.isFunction(m)){k=h+Math.floor(Math.random()*2147483648).toString(36);window[k]=j(0)}else{if(baidu.lang.isString(m)){k=m}else{if(g=c.exec(a)){k=g[2]}}}if(l){b=setTimeout(j(1),l)}a=a.replace(c,"\x241"+f+"="+k);if(a.search(c)<0){a+=(a.indexOf("?")<0?"?":"&")+f+"="+k}baidu.sio._createScriptTag(i,a,d);function j(p){return function(){try{if(p){o.onfailure&&o.onfailure()}else{m.apply(window,arguments);clearTimeout(b)}window[k]=null;delete window[k]}catch(q){}finally{baidu.sio._removeScriptTag(i)}}}};baidu.string.decodeHTML=function(a){var b=String(a).replace(/&quot;/g,'"').replace(/&lt;/g,"<").replace(/&gt;/g,">").replace(/&amp;/g,"&");return b.replace(/&#([\d]+);/g,function(d,c){return String.fromCharCode(parseInt(c,10))})};baidu.decodeHTML=baidu.string.decodeHTML;baidu.string.encodeHTML=function(a){return String(a).replace(/&/g,"&amp;").replace(/</g,"&lt;").replace(/>/g,"&gt;").replace(/"/g,"&quot;").replace(/'/g,"&#39;")};baidu.encodeHTML=baidu.string.encodeHTML;baidu.string.format=function(c,a){c=String(c);var b=Array.prototype.slice.call(arguments,1),d=Object.prototype.toString;if(b.length){b=b.length==1?(a!==null&&(/\[object Array\]|\[object Object\]/.test(d.call(a)))?a:b):b;return c.replace(/#\{(.+?)\}/g,function(e,g){var f=b[g];if("[object Function]"==d.call(f)){f=f(g)}return("undefined"==typeof f?"":f)})}return c};baidu.format=baidu.string.format;baidu.string.getByteLength=function(a){return String(a).replace(/[^\x00-\xff]/g,"ci").length};baidu.string.subByte=function(c,b,a){c=String(c);a=a||"";if(b<0||baidu.string.getByteLength(c)<=b){return c+a}c=c.substr(0,b).replace(/([^\x00-\xff])/g,"\x241 ").substr(0,b).replace(/[^\x00-\xff]$/,"").replace(/([^\x00-\xff]) /g,"\x241");return c+a};baidu.swf=baidu.swf||{};baidu.swf.version=(function(){var h=navigator;if(h.plugins&&h.mimeTypes.length){var d=h.plugins["Shockwave Flash"];if(d&&d.description){return d.description.replace(/([a-zA-Z]|\s)+/,"").replace(/(\s)+r/,".")+".0"}}else{if(window.ActiveXObject&&!window.opera){for(var b=12;b>=2;b--){try{var g=new ActiveXObject("ShockwaveFlash.ShockwaveFlash."+b);if(g){var a=g.GetVariable("$version");return a.replace(/WIN/g,"").replace(/,/g,".")}}catch(f){}}}}})();baidu.swf.createHTML=function(s){s=s||{};var j=baidu.swf.version,g=s.ver||"6.0.0",f,d,e,c,h,r,a={},o=baidu.string.encodeHTML;for(c in s){a[c]=s[c]}s=a;if(j){j=j.split(".");g=g.split(".");for(e=0;e<3;e++){f=parseInt(j[e],10);d=parseInt(g[e],10);if(d<f){break}else{if(d>f){return""}}}}else{return""}var m=s.vars,l=["classid","codebase","id","width","height","align"];s.align=s.align||"middle";s.classid="clsid:d27cdb6e-ae6d-11cf-96b8-444553540000";s.codebase="http://fpdownload.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,0,0";s.movie=s.url||"";delete s.vars;delete s.url;if("string"==typeof m){s.flashvars=m}else{var p=[];for(c in m){r=m[c];p.push(c+"="+encodeURIComponent(r))}s.flashvars=p.join("&")}var n=["<object "];for(e=0,h=l.length;e<h;e++){r=l[e];n.push(" ",r,'="',o(s[r]),'"')}n.push(">");var b={wmode:1,scale:1,quality:1,play:1,loop:1,menu:1,salign:1,bgcolor:1,base:1,allowscriptaccess:1,allownetworking:1,allowfullscreen:1,seamlesstabbing:1,devicefont:1,swliveconnect:1,flashvars:1,movie:1};for(c in s){r=s[c];c=c.toLowerCase();if(b[c]&&(r||r===false||r===0)){n.push('<param name="'+c+'" value="'+o(r)+'" />')}}s.src=s.movie;s.name=s.id;delete s.id;delete s.movie;delete s.classid;delete s.codebase;s.type="application/x-shockwave-flash";s.pluginspage="http://www.macromedia.com/go/getflashplayer";n.push("<embed");var q;for(c in s){r=s[c];if(r||r===false||r===0){if((new RegExp("^salign\x24","i")).test(c)){q=r;continue}n.push(" ",c,'="',o(r),'"')}}if(q){n.push(' salign="',o(q),'"')}n.push("></embed></object>");return n.join("")};baidu.swf.create=function(a,c){a=a||{};var b=baidu.swf.createHTML(a)||a.errorMessage||"";if(c&&"string"==typeof c){c=document.getElementById(c)}baidu.dom.insertHTML(c||document.body,"beforeEnd",b)};baidu.swf.getMovie=function(c){var a=document[c],b;return baidu.browser.ie==9?a&&a.length?(b=baidu.array.remove(baidu.lang.toArray(a),function(d){return d.tagName.toLowerCase()!="embed"})).length==1?b[0]:b:a:a||window[c]};baidu.url=baidu.url||{};baidu.url.getQueryValue=function(b,c){var d=new RegExp("(^|&|\\?|#)"+baidu.string.escapeReg(c)+"=([^&#]*)(&|\x24|#)","");var a=b.match(d);if(a){return a[2]}return null};T.undope=true;/**
 * 一些基础方法,对于 tangram 中没有或
 * 需要重写tangram的方法请扩展到这个文件中。
 *
 * @author zhangshuai
 * @date 2011-11-28
 */

(function(){

	var _ = baidu;
	
	//缓存全局方法
	var slice = Array.prototype.slice;
	var each = T.each;
	
	_.extend = function(target) {
		each(slice.call(arguments, 1), function(source){
			for (var i in source) {
				target[i] = source[i];
			}
		});
		return target;
	};
	

/*	_.date = _.date || {};
	_.date.shortFormat = function(d,now) {
		if (now) {
			if (typeof now != "number") {
				now = parseInt(now,10);
			}
			var today = new Date(now);
		} else {
			var today = new Date();
		}
		var mins = (today - d) / 1000 / 60;
		
		if (mins >= 0 && mins < 1) {
			return "刚刚";
		} else if (mins < 60) {
			return T.string.format('#{0}分钟前', Math.floor(mins));
		} else if (mins < 60 * 24) {
			return T.date.format(d, '今天 HH:mm');
		} else {
			return T.date.format(d, 'MM月dd日 HH:mm'); 
		}
	};
*/
	
	_.jsonp = (function(){
		var 
		script,
		cbindex = 0,
		cbprefix = "_mvcjsonpcb";


		function getCallback(opt) {
			return function(json) {
				opt.onsuccess(null,json);
			}
		}

		function request(url, opt){
			var head = document.head || document.getElementsByTagName( "head" )[0] || document.documentElement;
			script = document.createElement("script");
			script.async = "async";
			if (opt.scriptCharset) {
				script.charset = opt.scriptCharset;
			}
			var cbname = cbprefix + (cbindex++);
			window[cbname] = getCallback(opt);
			url = /\?/.test(url) ? url + "callback=" + cbname
								: url + "?callback=" + cbname;
			script.src = url;
			script.onload = script.onreadstatechange = function(_, isAbort) {
				if (isAbort || !script.readyState || /loaded|complete/.test(script.readState) ) {
					script.onload = script.onreadystatechange = null;
					if (head && script.parentNode) {
						head.removeChild(script);
					}
					script = undefined;
					window[cbname] = undefined;
				}
			};
			head.insertBefore( script, head.firstChild );
		}

		function abort(){
			if (script) {
				script.onload(0,1);
			}
		}

		return {
			abort : abort,
			request : request
		}
	
	})();

	_.string.decodeHTML = function(source){
		var str = String(source)
			.replace(/&quot;/g,'"')
			.replace(/&lt;/g,'<')
			.replace(/&gt;/g,'>')
			.replace(/&amp;/g, "&")
			.replace(/&mdash;/g, "—")
			.replace(/&ldquo;/g, "“")
			.replace(/&rdquo;/g, "”")
			;
		return str.replace(/&#([\d]+);/g, function(_0, _1){
			return String.fromCharCode(parseInt(_1, 10));
		});
	};

	/**
	 * 重写 baidu.ajax.request, 以支持跨二级域请求
	 * 
	 * 添加如下option
	 * options.crossSubDomain {bool}{default:false} 启动二级跨域请求
	 * options.proxyIframeId {string}	代理页面iframeId。 要求代理页面window对象必须有getXHR方法，用于获取xhr对象。
	 */
	_.ajax.request = function (url, opt_options) {
	    var options     = opt_options || {},
	        data        = options.data || "",
	        async       = !(options.async === false),
	        username    = options.username || "",
	        password    = options.password || "",
	        method      = (options.method || "GET").toUpperCase(),
	        headers     = options.headers || {},
	        // 基本的逻辑来自lili同学提供的patch
	        timeout     = options.timeout || 0,
	        eventHandlers = {},
	        tick, key, xhr;


		
	    /* <--跨二级域名支持代码  */
		//如果代理iframe还没有加载，这延时200毫秒执行
		if (options.crossSubDomain) {
	        var proxyIframe = baidu.g(options.proxyIframeId);
			try {
				if (!proxyIframe.contentWindow.getXHR) {
					setTimeout(function(){
						_.ajax.request(url, opt_options);	
					}, 200);
					return;
				}	
			} catch (e) {
				setTimeout(function(){
					_.ajax.request(url, opt_options);	
				}, 200);
				return;
			}
		}

	    /* 跨二级域名支持代码 --> */

	    /**
	     * readyState发生变更时调用
	     * 
	     * @ignore
	     */
	    function stateChangeHandler() {
	        if (xhr.readyState == 4) {
	            try {
	                var stat = xhr.status;
	            } catch (ex) {
	                // 在请求时，如果网络中断，Firefox会无法取得status
	                fire('failure');
	                return;
	            }
	            
	            fire(stat);
	            
	            // http://www.never-online.net/blog/article.asp?id=261
	            // case 12002: // Server timeout      
	            // case 12029: // dropped connections
	            // case 12030: // dropped connections
	            // case 12031: // dropped connections
	            // case 12152: // closed by server
	            // case 13030: // status and statusText are unavailable
	            
	            // IE error sometimes returns 1223 when it 
	            // should be 204, so treat it as success
	            if ((stat >= 200 && stat < 300)
	                || stat == 304
	                || stat == 1223) {
	                fire('success');
	            } else {
	                fire('failure');
	            }
	            
	            /*
	             * NOTE: Testing discovered that for some bizarre reason, on Mozilla, the
	             * JavaScript <code>XmlHttpRequest.onreadystatechange</code> handler
	             * function maybe still be called after it is deleted. The theory is that the
	             * callback is cached somewhere. Setting it to null or an empty function does
	             * seem to work properly, though.
	             * 
	             * On IE, there are two problems: Setting onreadystatechange to null (as
	             * opposed to an empty function) sometimes throws an exception. With
	             * particular (rare) versions of jscript.dll, setting onreadystatechange from
	             * within onreadystatechange causes a crash. Setting it from within a timeout
	             * fixes this bug (see issue 1610).
	             * 
	             * End result: *always* set onreadystatechange to an empty function (never to
	             * null). Never set onreadystatechange from within onreadystatechange (always
	             * in a setTimeout()).
	             */
	            window.setTimeout(
	                function() {
	                    // 避免内存泄露.
	                    // 由new Function改成不含此作用域链的 baidu.fn.blank 函数,
	                    // 以避免作用域链带来的隐性循环引用导致的IE下内存泄露. By rocy 2011-01-05 .
	                    xhr.onreadystatechange = baidu.fn.blank;
	                    if (async) {
	                        xhr = null;
	                    }
	                }, 0);
	        }
	    }
	    
	    /**
	     * 获取XMLHttpRequest对象
	     * 
	     * @ignore
	     * @return {XMLHttpRequest} XMLHttpRequest对象
	     */
	    function getXHR() {
	    	/* <-- 跨二级域名支持代码 */
	    	if (options.crossSubDomain) {
					var proxyIframe = baidu.g(options.proxyIframeId);
	    		if (!proxyIframe){
	    			SRX.debug("T.ajax.request: proxy iframe not found")
	    			return null;
	    		}
	    		try {
	    			var xhr = proxyIframe.contentWindow.getXHR();
	    		} catch (e) {
	    			SRX.debug(e);
	    			return null;
	    		}
	    		return xhr;
	    	}
	    	/* 跨二级域名支持代码 --> */
	    	
	        if (window.ActiveXObject) {
	            try {
	                return new ActiveXObject("Msxml2.XMLHTTP");
	            } catch (e) {
	                try {
	                    return new ActiveXObject("Microsoft.XMLHTTP");
	                } catch (e) {}
	            }
	        }
	        if (window.XMLHttpRequest) {
	            return new XMLHttpRequest();
	        }
	    }
	    
	    /**
	     * 触发事件
	     * 
	     * @ignore
	     * @param {String} type 事件类型
	     */
	    function fire(type) {
	        type = 'on' + type;
	        var handler = eventHandlers[type],
	            globelHandler = baidu.ajax[type];
	        
	        // 不对事件类型进行验证
	        if (handler) {
	            if (tick) {
	              clearTimeout(tick);
	            }

	            if (type != 'onsuccess') {
	                handler(xhr);
	            } else {
	                //处理获取xhr.responseText导致出错的情况,比如请求图片地址.
	                try {
	                    xhr.responseText;
	                } catch(error) {
	                    return handler(xhr);
	                }
	                handler(xhr, xhr.responseText);
	            }
	        } else if (globelHandler) {
	            //onsuccess不支持全局事件
	            if (type == 'onsuccess') {
	                return;
	            }
	            globelHandler(xhr);
	        }
	    }
	    
	    
	    for (key in options) {
	        // 将options参数中的事件参数复制到eventHandlers对象中
	        // 这里复制所有options的成员，eventHandlers有冗余
	        // 但是不会产生任何影响，并且代码紧凑
	        eventHandlers[key] = options[key];
	    }
	    
	    headers['X-Requested-With'] = 'XMLHttpRequest';
	    

	    try {
	        xhr = getXHR();
	        
	        if (method == 'GET') {
	            if (data) {
	                url += (url.indexOf('?') >= 0 ? '&' : '?') + data;
	                data = null;
	            }
	            if(options['noCache'])
	                url += (url.indexOf('?') >= 0 ? '&' : '?') + 'b' + (+ new Date) + '=1';
	        }
	        
	        if (username) {
	            xhr.open(method, url, async, username, password);
	        } else {
	            xhr.open(method, url, async);
	        }
	        
	        if (async) {
	            xhr.onreadystatechange = stateChangeHandler;
	        }
	        
	        // 在open之后再进行http请求头设定
	        // FIXME 是否需要添加; charset=UTF-8呢
	        if (method == 'POST') {
	            xhr.setRequestHeader("Content-Type",
	                (headers['Content-Type'] || "application/x-www-form-urlencoded"));
	        }
	        
	        for (key in headers) {
	            if (headers.hasOwnProperty(key)) {
	                xhr.setRequestHeader(key, headers[key]);
	            }
	        }
	        
	        fire('beforerequest');

	        if (timeout) {
	          tick = setTimeout(function(){
	            xhr.onreadystatechange = baidu.fn.blank;
	            xhr.abort();
	            fire("timeout");
	          }, timeout);
	        }
	        xhr.send(data);
	        
	        if (!async) {
	            stateChangeHandler();
	        }
	    } catch (ex) {
	        fire('failure');
	    }
	    
	    return xhr;
	};
	
}).call(this);

window.SRX=window.SRX||{},SRX.VERSION="1.0",SRX.config={debug:!1,DOMAIN:"3ren.cn",GLOBALDOMAIN:"http://www.3ren.cn",CLASSDOMAIN:"http://class.3ren.cn",SCHOOLDOMAIN:"http://school.3ren.cn",PHOTODOMAIN:"http://photo.srxign.com",BLOGDOMAIN:"http://blog.3ren.cn",SHAREDOMAIN:"http://share.3ren.cn",PROXY_IFRAME_ID:"jsonproxy",MAX_INPUT_LENGTH:140},SRX.extend=function(a){var b=Array.prototype.slice.call(arguments,1);for(var c=0;c<b.length;c++){var d=b[c];for(var e in d)d.hasOwnProperty(e)&&(a[e]=d[e])}return a},SRX.log=function(){if(window.console&&console.log){var a=arguments,b=a.length;for(var c=0;c<b;c++)console.log(a[c])}},SRX.debug=function(a){SRX.config.debug&&SRX.log.apply(this,arguments)},SRX.isUrl=function(a){return SRX.log("SRX.isUrl","这个方法在v2版中以废弃"),/^(https?|ftp|rmtp|mms):\/\/(([A-Z0-9][A-Z0-9_-]*)(\.[A-Z0-9][A-Z0-9_-]*)+)(:(\d+))?\/?/i.test(a)},SRX.isEmail=function(a){return SRX.log("SRX.isEmail","这个方法在v2版中以废弃"),/^[\w!#\$%'\*\+\-\/=\?\^`{}\|~]+([.][\w!#\$%'\*\+\-\/=\?\^`{}\|~]+)*@[-a-z0-9]{1,20}[.][a-z0-9]{1,10}([.][a-z]{2})?$/i.test(a)},SRX.absurl=function(a,b){return SRX.log("SRX.absurl","这个方法在v2版中以废弃"),/^http:/i.test(b)?b:a+b},SRX.alertEmpty=function(a){SRX.log("SRX.alertEmpty","这个方法在v2版中以废弃"),a=T.g(a),a.style.backgroundColor="#ffeeee",setTimeout(function(){a.style.backgroundColor=""},100),setTimeout(function(){a.style.backgroundColor="#ffeeee"},200),setTimeout(function(){a.style.backgroundColor=""},300)},SRX.enablebtn=function(a){SRX.log("SRX.enablebtn","这个方法在v2版中以废弃"),a=T.g(a),a.className=a.className.replace(/(button\d{2})-disabled/,"$1")},SRX.disablebtn=function(a){SRX.log("SRX.disablebtn","这个方法在v2版中以废弃"),a=T.g(a),a.className=a.className.replace(/(button\d{2})/,"$1-disabled")},SRX.isDisabled=function(a){return SRX.log("SRX.isDisabled","这个方法在v2版中以废弃"),a=T.g(a),/button\d{2}-disabled/.test(a.className)},SRX.fireClick=function(a){SRX.log("SRX.fireClick","这个方法在v2版中以废弃"),a=T.g(a);if(T.browser.isWebkit){var b=document.createEvent("Events");b.initEvent("click",!0,!0),a.dispatchEvent(b)}else a.click()},SRX.placeholder=function(){function b(){this.value.replace(/^\s*|\s*$/g,"")===""?(this.value=this.getAttribute("placeholder"),this.setAttribute("data-prevcolor",this.style.color),this.style.color=a):this.value!=this.getAttribute("placeholder")&&(this.style.color=this.getAttribute("data-prevcolor"))}function c(){this.value==this.getAttribute("placeholder")&&(this.value=""),this.style.color=this.getAttribute("data-prevcolor")}function d(){return!1}SRX.log("SRX.placeholder","这个方法在v2版中以废弃");var a="#AAA";return d()?function(a,b){a=T.g(a),a&&b&&(a.placeholder=b)}:function(d,e){var f=T.event.on;d=T.g(d),e&&d.setAttribute("placeholder",e);var g=d.getAttribute("placeholder");f(d,"focus",c),f(d,"blur",b);if(d.value===""||d.value==g)d.value=g,d.setAttribute("data-prevcolor",d.style.color),d.style.color=a}}(),SRX.splitString=function(a,b){return SRX.log("SRX.splitString","这个方法在v2版中以废弃"),T.string.getByteLength(a)<=2*b?a:T.string.subByte(a,2*b-1)+"..."},SRX.alert=function(a,b){SRX.log("SRX.alert","这个方法在v2版中以废弃");var c;if(!SRX.alert._popup){c=new SRX._Popup({className:"ui_alert",modal:!0,fade:!/MSIE/.test(window.navigator.userAgent),fixed:!0});var d="<div class='ui_popup_panel'><div class='ui_alert_body'></div><div class='ui_alert_foot'><a class='button24' href='javascript:;'><em>确定</em></a></div></div>";c.wrapper.innerHTML=d,c.wrapper.children[0].children[1].children[0].onclick=function(){c.close()},SRX.alert._popup=c}c=SRX.alert._popup,c.wrapper.children[0].children[0].innerHTML=a,T.browser.ie==6?c.wrapper.style.top=T.page.getScrollTop()+T.page.getViewHeight()*.4+"px":c.wrapper.style.top=T.page.getViewHeight()*.4+"px",c.open({modal:b})},SRX.broader={on:function(a,b,c){var d=this._callbacks||(this._callbacks={}),e=d[a]||(d[a]={}),f=e.tail||(e.tail=e.next={});return f.callback=b,f.context=c,e.tail=f.next={},this},un:function(a,b){var c,d,e;if(!a)this._callbacks={};else if(c=this._callbacks)if(!b)c[a]={};else if(d=c[a])while((e=d)&&(d=d.next)){if(d.callback!==b)continue;e.next=d.next,d.context=d.callback=null;break}return this},fire:function(a){var b,c,d,e,f,g=["all",a];if(!(c=this._callbacks))return this;while(f=g.pop()){if(!(b=c[f]))continue;e=f=="all"?arguments:Array.prototype.slice.call(arguments,1);while(b=b.next)(d=b.callback)&&d.apply(b.context||this,e)}return this}},SRX.showmask=function(){SRX.log("SRX.showmask","这个方法在v2版中以废弃");var a=SRX._showmaskcount;a===0&&(SRX.showmask.mask=new SRX._Mask,SRX.showmask.mask.open()),SRX._showmaskcount++},SRX._showmaskcount=0,SRX.closemask=function(){SRX.log("SRX.closemask","这个方法在v2版中以废弃");var a=SRX._showmaskcount;a==1?(SRX.showmask.mask.close(),SRX._showmaskcount=0):a>1&&SRX._showmaskcount--},SRX.info=function(a,b,c){SRX.log("SRX.info","这个方法在v2版中以废弃"),SRX.info._popup||(SRX.info._popup=new SRX._Popup({className:"ui_info",fade:!1}),SRX.info._popup.wrapper.innerHTML="<div class='ui_info_type'></div><p></p>");var d=2e3,e,f=SRX.info._popup;b=b||"success";var g=f.wrapper.className.split(" "),h=[];for(var i=0,j=g.length;i<j;i++)/ui_info_type_*/.test(g[i])||h.push(g[i]);h.push("ui_info_type_"+b),f.wrapper.className=h.join(" "),f.wrapper.children[1].innerHTML=a,f.wrapper.style.top=T.page.getScrollTop()+T.page.getViewHeight()*.4+"px",f.open(),e&&clearTimeout(e),e=setTimeout(function(){f.close()},c||d)},SRX.trackEvent=function(a,b,c,d){if(!_hmt)return;_hmt.push(["_trackEvent",a,b,c,d]),SRX.debug("[trackEvent]","\tcategory:"+a,"\taction:"+b,"\tlabel:"+c,"\tvalue:"+d)},SRX.trackPageview=function(a,b){if(!_hmt||!a)return;if(b)for(var c in b)b.hasOwnProperty(c)&&(a=a.replace(new RegExp("{{"+c+"}}","ig"),b[c]));_hmt.push(["_trackPageview",a]),SRX.debug("[trackPageview]","\turl:"+a)},SRX.showlogin=function(){function d(){SRX.closemask(),T.g("ui_showlogin").style.display="none",T.removeClass(T.g("ui_showlogin"),"fadeInDown"),T.g("ui_showloginHandle").style.display="none"}function e(){var a=T.g("ui_showlogin"),b=T.g("ui_showloginHandle");a.style.top=b.style.top="200px",a.style.left=b.style.left=(document.body.offsetWidth-a.offsetWidth)/2+"px",b.style.width=a.offsetWidth+"px",b.style.display="",SRX.fx.scrollTo(0),T.g("login_username").focus()}function f(a){a=a||window.event;var b=a.target||a.srcElement;b.tagName=="INPUT"&&b.focus(),b.tagName=="EM"&&(b=b.parentNode);if(!b.getAttribute("data-action"))return;switch(b.getAttribute("data-action")){case"close":d();break;case"login":T.g("dlgLoginForm").submit()}}function g(){var a=T.g("login_username"),b=T.g("login_userpwd"),d=T.g("login_target"),e=T.g("dlgLoginInfo");return d.value=location.href,a.value.replace(/ /g,"")===""||a.value==c?(e.innerHTML="请输入登录名",a.focus(),setTimeout(function(){e.innerHTML=""},2e3),!1):b.value===""||b.value.length<1?(e.innerHTML="请输入密码",b.focus(),setTimeout(function(){e.innerHTML=""},2e3),!1):!0}function h(a){a=a||window.event,document.onmouseup=i,a.preventDefault&&a.preventDefault();var b=this;b.className="handle handle_md",b.style.height=T.g("ui_showlogin").offsetHeight+"px"}function i(){var a=T.g("ui_showloginHandle"),b=a.children[0];b.style.height="",b.className="handle";var c=T.g("ui_showlogin");c.style.left=a.style.left,c.style.top=a.style.top}function j(){T.addClass(this,"hover")}function k(){T.removeClass(this,"hover")}function l(){var a=T.g("ui_showlogin");a.onclick=f,a.getElementsByTagName("form")[0].onsubmit=g,SRX.placeholder(T.g("login_username"),c);var b=T.g("ui_showloginHandle");ui_handle=b.children[0],T.dom.draggable(b,{handle:ui_handle}),T.event.on(ui_handle,"mousedown",h),T.browser.ie&&(T.event.on(ui_handle,"mouseover",j),T.event.on(ui_handle,"mouseout",k))}function m(){var c;SRX.showmask();if(T.g("ui_showlogin")){c=T.g("ui_showlogin"),c.style.display="",T.addClass(c,"fadeInDown"),e();return}c=document.createElement("div"),c.id="ui_showlogin",c.className="dlg-login",c.innerHTML=a;var d=document.createElement("div");d.id="ui_showloginHandle",d.className="dlg-login-handle",d.innerHTML=b,document.body.appendChild(c),document.body.appendChild(d),l(),e()}SRX.log("SRX.showlogin","这个方法在v2版中以废弃");var a=['<div class="ui_box">','<div class="ui_bg"></div>','<div class="ui_ct clearfix">','<div class="ui_login">','<form id="dlgLoginForm" autocomplete="false" action="http://www.3ren.cn/login" method="post">',"<strong>登录</strong>",'<div class="h_item input_wrap">','<label for="login_username">登录名</label>','<input type="text" class="ipttxt" id="login_username" placeholder="昵称/手机号/邮箱" name="userName" />','</div><div class="h_item input_wrap">','<label for="login_userpwd">密&nbsp;&nbsp;码</label>','<input type="password" class="ipttxt" name="password" id="login_userpwd" />',"</div>",'<input type="hidden" name="target" id="login_target" value="" />','<div class="dlg-login-auto">','<p class="dlg-login-info" id="dlgLoginInfo"></p>','<input type="checkbox" id="dlgLoginSaveStatus" name="saveStatus"/>','<label for="dlgLoginSaveStatus">下次自动登录</label>',"</div>",'<div class="h_item submit_btn">','<a href="http://www.srxing.com/reg/findpwd/index">忘记密码？</a>&nbsp;&nbsp;','<a href="javascript:;" data-action="login" class="button27 btn_submit">',"<em>登录</em>",'<input style="visibility:hidden;" type="submit" value="" />',"</a>","</div>","</form>","</div>",'<div class="ui_regist">',"<p>没有三人行账号？</p>",'<a href="http://www.srxing.com/register" class="button27"><em>立即注册</em></a>',"</div>",'<a href="javascript:;" data-action="close" class="ui_close">×</a>',"</div>","</div>"].join(""),b=['<div class="handle"></div>'].join(""),c="昵称/手机号/邮箱";return m}(),SRX._Mask=function(a){var b=SRX.extend({color:"black",opacity:.33,zindex:1e4,fadeDuration:200,fade:!0},a);this.color=b.color,this.opacity=b.opacity,this.zindex=b.zindex,this.fadeDuration=b.fadeDuration,this.fade=b.fade,this.resizeFunc=function(a){return function(){var b=window,c=document,d=c.documentElement,e=T.page;a.overlay&&(a.overlay.style.width=e.getWidth()+"px",a.overlay.style.height=e.getHeight()+"px")}}(this)},SRX._Mask.prototype={init:function(){var a=document,b=a.documentElement,c=window,d=T.page;this.overlay=document.createElement("div");var e=this.overlay;e.style.width=d.getWidth()+"px",e.style.height=d.getHeight()+"px",e.style.position="fixed",e.style.overflow="hidden",e.style.left=0,e.style.top=0,e.style.backgroundColor=this.color,e.style.zIndex=this.zindex,e.style.opacity=this.opacity,e.style.filter="alpha(opacity="+this.opacity*100+")",T.browser.ie==6&&(e.style.position="absolute",e.innerHTML='<iframe style="position:absolute;top:0;left:0;width:100%;height:100%;z-index=-1;filter:alpha(opacity=0);"></iframe>')},open:function(){this.overlay||this.init();var a=this;this.fade?SRX.fx.animate({el:this.overlay,property:"opacity",from:0,to:this.opacity,duration:this.fadeDuration,timeFunc:"easeOutExpo",onStart:function(){a.overlay.style.opacity=0,a.overlay.style.filter="alpha(opacity=0)",document.body.appendChild(a.overlay)}}):(this.overlay.style.display="none",document.body.appendChild(this.overlay),this.overlay.style.display=""),T.event.on(window,"resize",this.resizeFunc)},close:function(){if(this.overlay){var a=this;this.fade?SRX.fx.animate({el:this.overlay,property:"opacity",from:this.opacity,to:0,duration:this.fadeDuration,timeFunc:"easeOutExpo",onEnd:function(){document.getElementsByTagName("body")[0].removeChild(a.overlay),a.overlay=null}}):(document.body.removeChild(this.overlay),this.overlay=null),T.event.un(window,"resize",this.resizeFunc)}}},SRX.confirm=function(a,b,c,d){SRX.log("SRX.confirm","这个方法在v2版中以废弃");var e;if(!SRX.confirm._popup){e=new SRX._Popup({className:"ui_confirm",modal:!0,fixed:!0,fade:!/MSIE/.test(window.navigator.userAgent)});var f="<div class='ui_popup_panel'><div class='ui_confirm_body'></div><div class='ui_confirm_foot'><a class='button24' href='javascript:;'><em>确定</em></a>&nbsp;<a class='button24' href='javascript:;'><em>取消</em></a></div></div>";e.wrapper.innerHTML=f,e.wrapper.children[0].children[1].children[0].onclick=function(){e.fire("ok")},e.wrapper.children[0].children[1].children[1].onclick=function(){e.fire("cancel")},e.on("close",function(){this.un("ok"),this.un("cancel")},e),SRX.confirm._popup=e}e=SRX.confirm._popup,e.wrapper.children[0].children[0].innerHTML=a,e.un("ok"),e.on("ok",function(){if(b)try{b()}catch(a){SRX.log("confirm: callback_ok exception\n"+a)}this.close()},e),e.un("cancel"),e.on("cancel",function(){if(c)try{c()}catch(a){SRX.log("confirm: callback_cencel exception\n"+a)}this.close()},e),T.browser.ie==6?e.wrapper.style.top=T.page.getScrollTop()+T.page.getViewHeight()*.4+"px":e.wrapper.style.top=T.page.getViewHeight()*.4+"px",e.open({modal:d})},SRX.hoverable=function(a,b){SRX.log("SRX.hoverable","这个方法在v2版中以废弃");var c;if(T.browser.ie!=6)return;a=T.g(a);if(!a)return;if(!b)switch(a.tagName){case"TABLE":c="TR";break;case"UL":case"OL":c="LI";break;default:c=a.tagName}else c=b;a.attachEvent("onmouseover",SRX.hoverable.getOnMouseOverFunc(c)),a.attachEvent("onmouseout",SRX.hoverable.getOnMouseOutFunc(c))},SRX.hoverable.getOnMouseOverFunc=function(a){return function(){var b=window.event.srcElement;while(b&&b.tagName!==a)b=b.parentNode;b&&T.dom.addClass(b,"hover")}},SRX.hoverable.getOnMouseOutFunc=function(a){return function(){var b=window.event.srcElement;while(b&&b.tagName!==a)b=b.parentNode;b&&T.dom.removeClass(b,"hover")}},SRX.hoverable=SRX.hoverable,function(){function i(a){this.el=a.el,this.property=a.property,this.from=this.now=a.from,this.to=a.to,this.total=this.to-this.from,this.duration=a.duration||400,this.timeFunc=this.getTimeFunc(a.timeFunc),this.unit=this.getUnit(),this.onEnd=a.onEnd,this.onStart=a.onStart,this.initRenderFunc()}function j(b){b.startTime=(new Date).getTime(),g.push(b),T.lang.isFunction(b.onStart)&&b.onStart(),a._start()}var a=SRX.fx={},b=13,c=300,d=document,e=T.dom.getStyle,f=function(a){return typeof a=="string"?d.getElementById(a):a},g=[],h;i.prototype={step:function(){var a=!0,b=(new Date).getTime();return b>this.startTime+this.duration?(this.now=this.to,this.render(),this.onEnd&&this.onEnd(this.el),a=!1):(this.update(b),this.render()),a},update:function(a){var b=this.timeFunc(a-this.startTime,this.from,this.total,this.duration);Math.abs(b)>1&&(b=b>0?Math.floor(b):Math.ceil(b)),this.now=b},render:function(){this.el.style[this.property]=this.now+this.unit},_renderOpacity:function(){this.el.style.filter="progid:DXImageTransform.Microsoft.Alpha(opacity:"+Math.floor(this.now*100)+")"},_renderOther:function(){this.el[this.property]=this.now},initRenderFunc:function(){this.property=="opacity"&&/MSIE/.test(navigator.userAgent)?this.render=this._renderOpacity:this.el.style[this.property]==null&&(this.render=this._renderOther)},timeFuncs:{linear:function(a,b,c,d){return b+a*c/d},easeOutCubic:function(a,b,c,d){return c*((a=a/d-1)*a*a+1)+b},easeOutSine:function(a,b,c,d){return c*Math.sin(a/d*(Math.PI/2))+b},easeOutExpo:function(a,b,c,d){return a==d?b+c:c*(-Math.pow(2,-10*a/d)+1)+b},easeOutElastic:function(a,b,c,d){var e=1.70158,f=0,g=c;return a===0?b:(a/=d)==1?b+c:(f||(f=d*.3),g<Math.abs(c)?(g=c,e=f/4):e=f/(2*Math.PI)*Math.asin(c/g),g*Math.pow(2,-10*a)*Math.sin((a*d-e)*2*Math.PI/f)+c+b)},easeOutBack:function(a,b,c,d,e){return e===undefined&&(e=1.70158),c*((a=a/d-1)*a*((e+1)*a+e)+1)+b},easeOutBounce:function(a,b,c,d){return(a/=d)<1/2.75?c*7.5625*a*a+b:a<2/2.75?c*(7.5625*(a-=1.5/2.75)*a+.75)+b:a<2.5/2.75?c*(7.5625*(a-=2.25/2.75)*a+.9375)+b:c*(7.5625*(a-=2.625/2.75)*a+.984375)+b}},getTimeFunc:function(a){switch(a){case"linear":return this.timeFuncs.linear;case"easeOutCubic":return this.timeFuncs.easeOutCubic;case"easeOutSine":return this.timeFuncs.easeOutSine;case"easeOutExpo":return this.timeFuncs.easeOutExpo;case"easeOutElastic":return this.timeFuncs.easeOutElastic;case"easeOutBack":return this.timeFuncs.easeOutBack;case"easeOutBounce":return this.timeFuncs.easeOutBounce;default:return this.timeFuncs.easeOutCubic}},getUnit:function(){return this.property=="opacity"?"":"px"}},a.animate=function(a){a.el=f(a.el);var b=new i(a);return a.delay>0?setTimeout(function(){j(b)},a.delay):j(b),this},a._click=function(){for(var b=g.length-1;b>=0;b--)g[b].step()||g.splice(b,1);g.length||a.stop()},a._start=function(){h||(h=setInterval(a._click,b))},a.stop=function(a,b){a=f(a);if(a){var c;if(!b)for(c=g.length-1;c>=0;c--)g[c].el===a&&g.splice(c,1);else for(c=g.length-1;c>=0;c--)g[c].el===a&&g[c].property==b&&g.splice(c,1)}else clearInterval(h),h=null,g.length=0},a.fadeIn=function(b,c,d){a.animate({el:b,duration:c,timeFunc:d,property:"opacity",from:0,to:1,onStart:function(){this.render(),this.el.style.display=""}})},a.fadeOut=function(b,c,d,e){a.animate({el:b,duration:c,timeFunc:d,property:"opacity",from:1,to:0,onEnd:function(){f(b).style.display="none",e&&e.call(f(b))}})},a.slideDown=function(b,c,d){a.animate({el:b,duration:c,timeFunc:d,property:"height",from:0,to:parseInt(e(b,"height"),10),onStart:function(){this.el.style.display=""}})},a.slideUp=function(b,c,d){a.animate({el:b,duration:c,timeFunc:d,property:"height",from:parseInt(e(b,"height"),10),to:0,onStart:function(){var a=parseInt(e(b,"height"),10);this.el.setAttribute("data-height",a)},onEnd:function(){this.el.style.height=f(b).getAttribute("data-height")+"px",this.el.style.display="none"}})},a.scrollTo=function(b,c,d){var e=T.browser.isWebkit?document.body:document.documentElement;a.stop(e),a.animate({el:e,duration:c,timeFunc:d||"easeOutExpo",property:"scrollTop",from:T.page.getScrollTop(),to:b})}}(),SRX._Popup=function(a){SRX.extend(this,SRX.broader);var b={id:null,className:"",html:"",position:null,modal:!1,closeButton:!1,appendTo:null};SRX.extend(b,a),this.fadeDuration=b.fadeDuration,this.appendTo=b.appendTo,this.fade=b.fade,this.isFirstOpen=!0,this.init(b)},SRX._Popup.prototype={init:function(a){this.modal=a.modal;var b=this,c=document,d=c.createElement("div");d.className="ui_popup "+a.className,a.id&&(d.id=a.id),d.innerHTML=a.html;if(a.closeButton){var e=document.createElement("a");e.href="javascript:;",e.className="close",e.title="关闭",e.innerHTML="×",d.appendChild(e),e.onclick=function(){b.close()}}a.position&&(d.style.left=a.position.x+"px",d.style.top=a.position.y+"px"),d.style.display="none",this.wrapper=d,a.modal&&(this.mask=new SRX._Mask({fadeDuration:a.fadeDuration,fade:a.fade}))},open:function(a){this.isFirstOpen&&(this.appendTo?T.g(this.appendTo).appendChild(this.wrapper):document.getElementsByTagName("body")[0].appendChild(this.wrapper),this.isFirstOpen=!1),this.fade?SRX.fx.fadeIn(this.wrapper,this.fadeDuration):this.wrapper.style.display="",this.fire("open");if(a&&a.modal===!1)return;this.modal&&this.mask.open()},close:function(){this.fade?SRX.fx.fadeOut(this.wrapper,this.fadeDuration):this.wrapper.style.display="none",this.modal&&this.mask.close(),this.fire("close")}};
