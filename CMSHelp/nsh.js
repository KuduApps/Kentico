// General page init functions for HM Premium Pack 1.2
// Copyright (c) 2009 by Tim Green and EC Software. 
// All rights reserved.
function addEvent(e,d,b,a){if(e.addEventListener){e.addEventListener(d,b,a);
return true
}else{if(e.attachEvent){var c=e.attachEvent("on"+d,b);
return c
}else{alert("Could not add event!")
}}}function trim(a){return a.replace(/^\s+|\s+$/g,"")
}function doResize(){var a,b;
if(self.innerHeight){a=self.innerHeight
}else{if(document.documentElement&&document.documentElement.clientHeight){a=document.documentElement.clientHeight
}else{if(document.body){a=document.body.clientHeight
}}}b=document.getElementById("idheader").offsetHeight;
if(a<b){a=b+1
}document.getElementById("idcontent").style.height=a-document.getElementById("idheader").offsetHeight+"px"
}function nsrInit(){if(self.innerHeight){document.getElementById("scriptNavHead").style.display="table-row"
}else{document.getElementById("scriptNavHead").style.display="block"
}document.getElementById("noScriptNavHead").style.display="none";
contentbody=document.getElementById("idcontent");
if(contentbody){contentbody.className="nonscroll";
document.getElementsByTagName("body")[0].className="nonscroll";
document.getElementsByTagName("html")[0].className="nonscroll"
}}function getHref(b,a,d){var e;
var c=document.location.href.replace(/\#.*$/,"");
c=c.replace(/\?.*?$/,"");
c=c.replace(/\/(?!.*?\/)/,"/"+b+"?");
e='<p class="help-url"><b>'+a+'&nbsp;</b><a href="'+c;
e=e+'" target="_top" title="'+d+'">'+c+"</a></p>";
return e
}function mailFB(g,f,h,e,c){var d=unQuot(g);
var j="mailto:"+escape(f)+"?subject="+d;
var a=unQuot(h);
var i="&body=Ref:%20"+a+"%20ID:%20"+e+"%0A%0D%0A%0D"+unQuot(c)+"%0A%0D%0A%0D";
var b=j+i;
return document.location.href=b
}function SearchCheck(){var c=window.location.search.lastIndexOf("zoom_highlight")>0;
if(!c){var a=document.getElementsByTagName("FONT");
if(a.length>0){var b="";
for(var d=0;
d<a.length;
d++){b=a[d].style.cssText;
if(b.indexOf("BACKGROUND-COLOR")==0){c=true;
break
}}}}return c
}function unQuot(a){a=a.replace(/'/g,"`");
a=a.replace(/&gt;/g,">");
a=a.replace(/&lt;/g,"<");
a=a.replace(/&quot;/g,'"');
a=a.replace(/&amp;/g,"&");
a=escape(a);
a=a.replace(/%E2|%E0|%E5|%E1|%E3/g,"a");
a=a.replace(/%C5|%C0|%C1|%C2|%C3/g,"A");
a=a.replace(/%C7/g,"C");
a=a.replace(/%E7/g,"c");
a=a.replace(/%E9|%EA|%EB|%E8/g,"e");
a=a.replace(/%C9|%CA|%C8|%CB/g,"E");
a=a.replace(/%u0192/g,"f");
a=a.replace(/%EF|%EE|%EC|%ED/g,"i");
a=a.replace(/%CF|%CD|%CE|%CC/g,"I");
a=a.replace(/%F1/g,"n");
a=a.replace(/%D1/g,"N");
a=a.replace(/%F4|%F2|%F3|%F5|%F8/g,"o");
a=a.replace(/%D4|%D2|%D3|%D5|%D8/g,"O");
a=a.replace(/%u0161/g,"s");
a=a.replace(/%u0160/g,"S");
a=a.replace(/%FB|%FA|%F9/g,"u");
a=a.replace(/%DB|%DA|%D9/g,"U");
a=a.replace(/%FF|%FD/g,"y");
a=a.replace(/%DD|%u0178/g,"Y");
a=a.replace(/%FC/g,"ue");
a=a.replace(/%DC/g,"Ue");
a=a.replace(/%E4|%E6/g,"ae");
a=a.replace(/%C4|%C6/g,"Ae");
a=a.replace(/%F6|%u0153/g,"oe");
a=a.replace(/%D6/g,"Oe");
a=a.replace(/%DF/g,"ss");
return(a)
}function toggleToggles(){if(HMToggles.length!=null){var a=true;
for(var b=0;
b<HMToggles.length;
b++){if(HMToggles[b].getAttribute("hm.state")=="1"){a=false;
break
}}HMToggleExpandAll(a)
}}function toggleCheck(d){var c=$(d[0]).parents("table[id^='TOGGLE']");
var a=c.size();
var e=false;
if(a>0){var j,h,f,g;
for(var b=0;
b<a;
b++){j=c[b];
h=$(j).attr("id");
f="$"+h+"_ICON";
g=j.getAttribute("hm.state");
if($("img[id='"+f+"']").attr("src")!=null){e=true
}if((g)=="0"||(g==null)){if(!e){HMToggle("toggle",h)
}else{HMToggle("toggle",h,f)
}}}}}function openTargetToggle(e,b){var a;
var d=false;
if(b=="menu"){a=$(e[0]).parent("span:has(a.dropdown-toggle)").find("a.dropdown-toggle").attr("href")
}else{a=$(e[0]).parent("p:has(a.dropdown-toggle)");
a=$(a).find("a.dropdown-toggle").attr("href");
if(!a){a=$(e[0]).parents("table:has(a.dropdown-toggle)")[0];
a=$(a).find("a.dropdown-toggle").attr("href")
}}var f=false;
var c="";
if(a){if(a.indexOf("ICON")!=-1){f=true
}a=a.replace(/^.*?\,\'/,"");
a=a.replace(/\'.*$/,"");
if(f){c="$"+a+"_ICON"
}if(!f){HMToggle("toggle",a);
return true
}else{HMToggle("toggle",a,c);
return true
}}else{return false
}}function toggleJump(){if(location.hash){var b=location.hash.replace(/\#/,"");
var a;
if($("a[id='"+b+"']").length>0){a=$("a[id='"+b+"']")
}else{if($("a[name='"+b+"']").length>0){a=$("a[name='"+b+"']")
}else{return false
}}if(HMToggles.length!=null){HMToggleExpandAll(false)
}toggleCheck(a);
if($(a).parent("p:not(:has(a.dropdown-toggle))").length==0){openTargetToggle(a,"page")
}$("#idcontent").scrollTo($(a),300,{offset:-12});
return false
}}$(document).ready(function(){var a=/msie 6|MSIE 6/.test(navigator.userAgent);
var b=document.location.pathname;
b=b.replace(/^.*[/\\]|[?#&].*$/,"");
$("a[href^='"+b+"#'],a[href^='#']:not(a[href='#'])").click(function(){var c=$(this).attr("href").replace(/.*?\#/,"");
var d=$("a[id='"+c+"']");
if(!d.length>0){d=$("a[name='"+c+"']")
}if(HMToggles.length!=null){HMToggleExpandAll(false)
}toggleCheck(d);
if($(d).parent("p:not(:has(a.dropdown-toggle))").length==0){openTargetToggle(d,"page")
}$("#idcontent").scrollTo($(d),600,{offset:-12});
return false
})
});