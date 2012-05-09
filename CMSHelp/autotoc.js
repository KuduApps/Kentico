// Auto-TOC script functions for HM Premium Pack 1.2
// Copyright (c) 2009 by Tim Green and EC Software. 
// All rights reserved.
addEvent(window,"load",autoTOC);
function truncate(d,a){var c,b;
if(a==0){return d
}if((a>0)&&(a<=20)){a=20
}c=d.split("");
if(c.length>a){for(b=c.length-1;
b>-1;
--b){if(b>a){c.length=b
}else{if(" "===c[b]){c.length=b;
break
}}}c.push("...")
}return c.join("")
}function htmlFix(a){heading=a.replace(/\&/g,"&amp;");
a=a.replace(/</g,"&lt;");
a=a.replace(/>/g,"&gt;");
return a
}function autoTOC(){var a=new Array();
var n=new Array();
var o=/msie|MSIE 6/.test(navigator.userAgent);
var l=initAtocVars();
var d=l.atoc_tip;
var c=l.atoc_minHeaders;
var v=l.atoc_btntip_on;
var h=l.atoc_toptip;
var g=l.atoc_top;
var x=l.atoc_bg;
var b=l.atoc_border;
var u=l.atoc_linkcolor;
var m=l.atoc_hovercolor;
var p=l.atoc_bgoffset;
var t,w,j,f,y,z,s,q,e,k=false;
$("span[class*='_atoc_']").parent("td:not(:has(span[class='temp_atoc_']))").each(function(){var i=$(this).html();
i='<span class="temp_atoc_">'+i+"</span>";
$(this).html(i)
});
$("span[class*='_atocs_']").parent("td:not(:has(span[class='temp_atocs_']))").each(function(){var i=$(this).html();
i='<span class="temp_atocs_">'+i+"</span>";
$(this).html(i)
});
a=$("p[class*='_atoc_'],p[class*='_atocs_'],span[class='temp_atoc_'],span[class='temp_atocs_'],");
if(a.length>=c){for(var r=0;
r<a.length;
r++){t=a[r];
w=$(a[r]).text();
w=trim(w);
w=htmlFix(w);
j=$(t).attr("class");
if(j.indexOf("_atocs_")!=-1){f=true
}else{f=false
}if(w.length==1){w=w.replace(/\xa0/,"")
}if(w!=""){k=true;
z="autoTOC"+r;
y=w.replace(/\"/g,"'");
w=truncate(w,35);
t.innerHTML='<a id="'+z+'"></a>'+t.innerHTML;
if(!f){s='<li class="autoTOC" id="src_'+z+'" title="'+d+y+'"><p class="autoTOC" style="color:'+u+';">'+w+"</p></li>"
}else{s='<li class="autoTOC" id="src_'+z+'" title="'+d+y+'"><p class="autoTOC" style="font-size: 90%; font-weight: normal;color:'+u+';">&nbsp;&nbsp;-&nbsp;'+w+"</p></li>"
}n.push(s)
}}}else{return
}if(k){if((n[0])&&(n[0]!="")){q="";
e=document.getElementById("autoTocWrapper");
for(var r=0;
r<n.length;
r++){q=q+n[r]
}q='<li id="toplink" title="'+h+'"><p class="autoTOC" style="color:'+u+';">'+g+"</p></li>"+q;
q='<div id="autoTocMiddle"><div id="autoTocInner"><ul>'+q+"</ul></div></div>";
e.innerHTML=q
}$(document).ready(function(){$.fn.tagName=function(){return this.get(0).tagName
};
$("div#autoTocInner").css("border-color",b);
$("#autoTocWrapper ul li").css("background-color",x);
var A='<img src="atoc.gif" border="0" title="'+v+'" />';
$("td#atocnav").html(A);
$("#atocnav img").mouseover(function(){$(this).attr("src","atoc_h.gif");
$(this).css("cursor","pointer")
}).mouseout(function(){$(this).attr("src","atoc.gif")
});
$("#atocnav").click(function(){var C=$("div#idheader").height()+4;
C=C+"px";
$("div#autoTocWrapper").css("top",C);
if(!o){$("#autoTocWrapper").slideToggle("fast")
}else{$("#autoTocWrapper").toggle()
}});
$("li.autoTOC").click(function(){var D=SearchCheck();
var C=$(this).attr("id");
var E=C.replace(/src_/,"");
var F=$("a[id='"+E+"']");
if((HMToggles.length!=null)&&(!D)){HMToggleExpandAll(false)
}if(!D){toggleCheck(F);
openTargetToggle(F,"menu")
}$("#idcontent").scrollTo($(F).parent(),600,{offset:-12,axis:"y"});
if(!jQuery.browser.msie){$("a[id='"+E+"']").parent().fadeTo(600,1).fadeTo(300,0.44).fadeTo(300,1).fadeTo(300,0.44).fadeTo(300,1)
}return false
});
$("#toplink").click(function(){var C=SearchCheck();
if(HMToggles.length!=null&&!C){HMToggleExpandAll(false)
}$("#idcontent").scrollTo(0,600);
return false
});
function i(){if(!o){$("#autoTocWrapper").slideUp("fast")
}else{$("#autoTocWrapper").hide()
}}var B=$("#innerdiv");
if(B[0]){addEvent(B[0],"click",i,false)
}$(window).bind("resize",function(){var C=$("div#idheader").height()+4;
C=C+"px";
$("div#autoTocWrapper").css("top",C)
});
$("#autoTocWrapper ul li").mouseover(function(){$(this).css("background-position",p);
$(this).children().filter("p.autoTOC").css("color",m)
});
$("#autoTocWrapper ul li").mouseout(function(){$(this).css("background-position","0px -27px");
$(this).children().filter("p.autoTOC").css("color",u)
})
})
}};