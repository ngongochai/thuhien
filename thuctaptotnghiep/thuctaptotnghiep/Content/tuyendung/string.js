function ltrim(_1){var _2=/^\s */;return _1.replace(_2,"");}
function rtrim(_3){var _4=/\s *$/;return _3.replace(_4,"");}
function trim(_5){return ltrim(rtrim(_5));}
function errorAlert(_6,_7){objErr=document.getElementById("err_top");if(objErr!=null){objErr.style.display="";}
if(_7==null){_7="err_"+_6.name;}
objErr=document.getElementById(_7);objErr.style.display="";return false;}
function errorAlertOff(_8,_9){if(_9==null){_9="err_"+_8.name;}
objErr=document.getElementById(_9);objErr.style.display="none";}
function checkRequired(_a,_b){if(_b==null){_b="err_"+_a.name;}
objErr=document.getElementById(_b);objErr.style.display="none";var s=_a.value;if(trim(s)==""){return errorAlert(_a,_b);}
return true;}
function jqCheckRequired(_a,_b){if(_b==null)
_b='err_'+_a.attr('id');if(trim(_a.val())==''){$('#'+_b).removeClass('hide');return false;}else{$('#'+_b).addClass('hide');return true;}}
function showFieldError(elementID){document.getElementById(elementID).style.display='';var objErr=document.getElementById("err_top");if(objErr!=null)objErr.style.display="";}
function hideFieldError(elementID){document.getElementById(elementID).style.display='none';}
function _checkTime(st){var s=trim(st);var i=s.indexOf(":");if(i<0){return false;}
var hh=s.substr(0,i);var mm=s.substr(i+1);if(!_checkInt(hh)){return false;}
if(!_checkInt(mm)){return false;}
h=hh.valueOf();m=mm.valueOf();if((h<0)||(h>23)){return false;}
if((m<0)||(m>59)){return false;}
return true;}
function checkTime(obj,_13){if(_13==null){_13="err_"+obj.name;}
objErr=document.getElementById(_13);objErr.style.display="none";var s=trim(obj.value);if(!_checkTime(s)){return errorAlert(obj,_13);}
return true;}
function _checkDate(st){var s=trim(st);var i1=s.indexOf("/");if(i1<0){return false;}
var i2=s.indexOf("/",i1+1);if(i2<0){return false;}
var mm=s.substr(0,i1);var dd=s.substr(i1+1,i2-(i1+1));var yy=s.substr(i2+1);if(!_checkInt(dd)){return false;}
if(!_checkInt(mm)){return false;}
if(!_checkInt(yy)){return false;}
d=dd.valueOf();m=mm.valueOf();y=yy.valueOf();if((m<1)||(m>12)){return false;}
if(y<1){return false;}
var _1c=(((y%4)==0&&(y%100)!=0)||(y%400)==0);var _1d=0;_1d=31;if((m==4)||(m==6)||(m==9)||(m==11)){_1d=30;}
if(m==2){_1d=(_1c)?29:28;}
if((dd.valueOf()<1)||(dd.valueOf()>_1d)){return false;}
return true;}
function checkDate(obj,_1f){if(_1f==null){_1f="err_"+obj.name;}
objErr=document.getElementById(_1f);objErr.style.display="none";var s=trim(obj.value);if(!_checkDate(s)){return errorAlert(obj,_1f);}
return true;}
function dateUTC(st){s=trim(st);var i1=s.indexOf("/");var i2=s.indexOf("/",i1+1);var mm=s.substr(0,i1);var dd=s.substr(i1+1,i2-(i1+1));var yy=s.substr(i2+1);return Date.UTC(yy,mm-1,dd);}
function lessDate(s1,s2){return(dateUTC(s1)<dateUTC(s2));}
function _checkInt(st){var _2a=trim(st);if(_2a.length<1||_2a==""){return false;}
var _2b="0123456789";for(i=0;i<_2a.length;i++){ch=_2a.charAt(i);if(_2b.indexOf(ch)<0){return false;}}
if(_2a.valueOf()<=0){return false;}
return true;}
function checkInt(obj,_2d){if(_2d==null){_2d="err_"+obj.name;}
objErr=document.getElementById(_2d);objErr.style.display="none";var s=trim(obj.value);if(!_checkInt(s)){return errorAlert(obj,_2d);}
return true;}
function _checkIntHaveZero(st){var _2a=trim(st);if(_2a.length<1||_2a==""){return false;}
var _2b="0123456789";for(i=0;i<_2a.length;i++){ch=_2a.charAt(i);if(_2b.indexOf(ch)<0){return false;}}
if(_2a.valueOf()<0){return false;}
return true;}
function checkIntHaveZero(obj,_2d){if(_2d==null){_2d="err_"+obj.name;}
objErr=document.getElementById(_2d);objErr.style.display="none";var s=trim(obj.value);if(_checkIntHaveZero(s)==false){return errorAlert(obj,_2d);}
return true;}
function _checkRange(st,_30,_31){var _32=trim(st);if(!_checkInt(_32)){return false;}
var v=_32.valueOf();return((v>=_30)&&(v<=_31));}
function checkRange(obj,_35,_36,_37){if(_35==null){_35="err_"+obj.name;}
objErr=document.getElementById(_35);objErr.style.display="none";var s=trim(obj.value);if(!_checkRange(s,_36,_37)){return errorAlert(obj,_35);}
return true;}
function _checkDouble(st){var _3a=trim(st);if(_3a.length<1){return false;}
var _3b="0123456789.";for(i=0;i<_3a.length;i++){ch=_3a.charAt(i);if(_3b.indexOf(ch)<0){return false;}}
if(_3a.indexOf(".")!=_3a.lastIndexOf(".")){return false;}
return true;}
function checkDouble(obj,_3d){if(_3d==null){_3d="err_"+obj.name;}
objErr=document.getElementById(_3d);objErr.style.display="none";var s=trim(obj.value);if(!_checkDouble(s)){return errorAlert(obj,_3d);}
return true;}
function _checkPhone(st){var _40="0123456789-+() ";var _41=trim(st);if(_41.length<7){return false;}
for(i=0;i<_41.length;i++){ch=_41.charAt(i);if(_40.indexOf(ch)<0){return false;}}
return true;}
function checkPhone(obj,_43){if(_43==null){_43="err_"+obj.name;}
objErr=document.getElementById(_43);objErr.style.display="none";var s=trim(obj.value);if(!_checkPhone(s)){return errorAlert(obj,_43);}
return true;}
function _checkMultiPhone(st){var _40="0123456789-+()/ ";var _41=trim(st);if(_41.length<1){return false;}
for(i=0;i<_41.length;i++){ch=_41.charAt(i);if(_40.indexOf(ch)<0){return false;}}
return true;}
function checkMultiPhone(obj,_43){if(_43==null){_43="err_"+obj.name;}
objErr=document.getElementById(_43);objErr.style.display="none";var s=trim(obj.value);if(!_checkMultiPhone(s)){return errorAlert(obj,_43);}
return true;}
function checkHaveEmailEditor(content,objname){objErr=document.getElementById("err_"+objname);objErr.style.display="none";content=content.replace(/[\s|\||\,|\;|\:]+/gi,' ');arr=content.split(" ");for(var i=0;i<arr.length;i++){s1=trim(arr[i]);if(_checkAtsign(s1)){return true;}}
return;}
function _checkAtsign(st){var _46="@";var _47=new RegExp(_46);return _47.exec(st);}
function _checkEmail(st){var _46="^[_a-zA-Z0-9-]+(\\.[_a-zA-Z0-9-]+)*@[a-zA-Z0-9-]+(\\.[a-zA-Z0-9-]+)*(\\.[a-zA-Z]{2,8})$";var _47=new RegExp(_46);return _47.exec(st);}
function checkEmail(obj,_49){if(_49==null){_49="err_"+obj.name;}
objErr=document.getElementById(_49);objErr.style.display="none";var s=trim(obj.value);if(!_checkEmail(s)){return errorAlert(obj,_49);}
return true;}
function _checkEmailArray(st){var s=trim(st);if(s.indexOf(";")<0&&s.indexOf(",")<0){return _checkEmail(s);}
var arr;var s1="";if(s.indexOf(";")>-1){arr=s.split(";");}else{arr=s.split(",");}
for(var i=0;i<arr.length;i++){s1=trim(arr[i]);if(!_checkEmailArray(s1)){return false;}}
return true;}
function checkEmailArray(obj,_51){if(_51==null){_51="err_"+obj.name;}
objErr=document.getElementById(_51);objErr.style.display="none";var s=trim(obj.value);var _53=s.charAt(s.length-1);if(_53==";"||_53==","){s=s.substr(0,s.length-1);}
if(!_checkEmailArray(s)){return errorAlert(obj,_51);}
return true;}
function _checkUpFile(_54){var _55=new Array("doc","html","htm","pdf","txt","ppt","dat","avi","mp3");var _56=_54.value.substr(_54.value.lastIndexOf(".")+1,_54.value.length);var _57=false;for(i=0;i<_55.length;i++){if(_56.toUpperCase()==_55[i].toUpperCase()){var _57=true;}}
if(_57==false){alert("Invalid file extension "+"."+_56+"!");return false;}
return true;}
function checkUpFile(obj,_59){if(_59==null){_59="err_"+obj.name;}
objErr=document.getElementById(_59);objErr.style.display="none";var s=trim(obj.value);if(!_checkUpFile(s)){return errorAlert(obj,_59);}
return true;}
function getCheckBoxIDs(frm,_5c){var sID="";for(i=0;i<frm.elements.length;i++){if(frm.elements[i].name==_5c){if(frm.elements[i].checked){sID+=","+frm.elements[i].value;}}}
if(sID.length>0){sID=sID.substr(1);}
return sID;}
function getListBoxIDs(lbx){var sID="";for(i=0;i<lbx.length;i++){if(lbx.options[i].selected){sID+=","+lbx.options[i].value;}}
if(sID.length>0){sID=sID.substr(1);}
return sID;}
function setListBoxIDs(lbx,_61){var _62=","+_61+",";var sID;for(i=0;i<lbx.length;i++){sID=","+lbx.options[i].value+",";if(_62.indexOf(sID)>-1){lbx.options[i].selected=true;}}}
function AddTo(_64,_65){for(k=0;k<_64.length;k++){IsExisted=false;if(_64.options[k].selected==true){stext=_64.options[k].text;svalue=_64.options[k].value;for(i=0;i<_65.length;i++){if(svalue==_65.options[i].value){IsExisted=true;break;}}
if(IsExisted==false){var _66=new Option(stext,svalue);j=_65.length;_65.options[j]=_66;}}}}
function RemoveFrom(_67){for(k=_67.length-1;k>-1;k--){if(_67.options[k].selected){_67.options[k]=null;}}}
function maxLength(_68,len){if(_68.value.length>len){_68.value=_68.value.substr(0,len);}}
function selectAll(_6a){for(i=0;i<_6a.length;i++){_6a.options[i].selected=true;}}
function inputFloat(_6b,_6c){if(_6b.value==""){return true;}
var i,c,state,value;state="H";value="";for(i=0;i<_6b.value.length;i++){c=_6b.value.charAt(i);if(c=="-"){if(i==0&&_6c==true){value+=c;}}else{if(c=="."){if(state=="H"){value+=c;state="T";}}else{if(c>="0"&&c<="9"){value+=c;}}}}
if(value.length>0&&value.charAt(0)=="."){value="0"+value;}
if(_6b.value!=value){_6b.value=value;}}
function inputInt(_6e,_6f){if(_6e.value==""){return true;}
var i,c,value;value="";for(i=0;i<_6e.value.length;i++){c=_6e.value.charAt(i);if(c=="-"){if(i==0&&_6f==true){value+=c;}}else{if(c>="0"&&c<="9"){value+=c;}}}
if(_6e.value!=value){_6e.value=value;}}
function openWin(url,_72,_73,_74){window.open(url,_72,"width="+_73+",height="+_74+",scrollbars=yes,toolbar=no,menubar=no,status=no,resizable=no");}
function _checkText(st){var _76="[^0-9]+$";var _77=new RegExp(_76);return _77.exec(st);}
function checkText(obj,_79){if(_79==null){_79="err_"+obj.name;}
objErr=document.getElementById(_79);objErr.style.display="none";var s=trim(obj.value);if(!_checkText(s)){return errorAlert(obj,_79);}
return true;}
function checkAllBoxes(_7b,_7c,_7d){var _7e=document.getElementById(_7b);if(_7e.checked==true){for(i=0;i<_7d;i++){var _7f=document.getElementById(_7c+i);if(_7f.disabled==false){_7f.checked=true;}}}else{for(i=0;i<_7d;i++){var _7f=document.getElementById(_7c+i);if(_7f.disabled==false){_7f.checked=false;}}}}
function checkBoxes(_80,_81,_82){var _83=0;for(i=0;i<_82;i++){var _84=document.getElementById(_81+i);if(_84.checked==true){_83++;}}
var _85=document.getElementById(_80);if(_83==_82){_85.checked=true;}else{_85.checked=false;}}
function _checkTextSpecial(st){var _86="[0-9@%#~\^\*\$\.]+";var _87=new RegExp(_86);return _87.exec(st);}
function checkTextSpecial(obj,_88){if(_88==null){_88="err_"+obj.name;}
objErr=document.getElementById(_88);objErr.style.display="none";var s=trim(obj.value);if(s==""){return errorAlert(obj,_88);}
if(_checkTextSpecial(s)){return errorAlert(obj,_88);}
return true;}
function limiter(inputId,displayId,languageId,limit){var text=document.getElementById(inputId).value;var len=text.length;if(len>limit){text=text.substring(0,limit);document.getElementById(inputId).value=text;return false;}
if(languageId==1){dTextPre='Bạn có thể nhập thêm ';dTextSuf=' ký tự';}else{dTextPre='You have ';dTextSuf=' characters remaining';}
$('#'+displayId).css('display','');$('#'+displayId).removeClass('hide');var remain=limit-len;document.getElementById(displayId).innerHTML=dTextPre+remain+dTextSuf;}
function removeHTMLTags(strInputCode){strInputCode=strInputCode.replace(/&(lt|gt);/g,function(strMatch,p1){return(p1=="lt")?"<":">";});var strTagStrippedText=strInputCode.replace(/<\/?[^>]+(>|$)/g,"");return strTagStrippedText;}
function emailExistsInString(_1){_1=removeHTMLTags(_1);_1=_1.replace(/\s+|-|\./g,'');var patt=/([a-z0-9_\.\-])+\@\w+[\.\w+]+\b/gi;var result=_1.match(patt);if(result==null)return false;else return result;}
function validateUrl(_1) {
    var _2 = "^(http:\/\/www\.|https:\/\/www\.|http:\/\/|https:\/\/)[a-z0-9]+([\-\.]{1}[a-z0-9]+)*\.[a-z]{2,5}(:[0-9]{1,5})?(\/.*)?$";
    var _3 = new RegExp(_2);
    var m = _3.exec(_1);
    if (m == null) {
        return false;
    } else {
        return true;
    }
}
function phoneExistsInString(_1){_1=removeHTMLTags(_1);_1=_1.replace(/\s+|-|\./g,'');var _2="[0-9]{8,12}";var re=new RegExp(_2);var m=re.exec(_1);if(m==null){return false;}else{return true;}}
function phoneExistsInStringSpecialChar(_1){_1=removeHTMLTags(_1);_1=_1.replace(/\s|\./g,'');var _2="[0-9]{8,12}";var re=new RegExp(_2);var m=re.exec(_1);if(m==null){return false;}else{return true;}}