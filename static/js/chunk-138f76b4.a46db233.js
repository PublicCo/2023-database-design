(window["webpackJsonp"]=window["webpackJsonp"]||[]).push([["chunk-138f76b4"],{"0496":function(e,t,n){"use strict";n.r(t);var i=function(){var e=this,t=e.$createElement,n=e._self._c||t;return n("page-layout",{attrs:{title:"员工个人主页"}},[n("a-card",{attrs:{bordered:!0}},[n("detail-list",[n("h1",{staticClass:"title"},[e._v("个人信息")]),n("a-row",[n("a-col",{attrs:{span:8}},[n("detail-list-item",{staticClass:"item_label",attrs:{term:"编号"}},[n("span",{staticClass:"info_display"},[e._v(e._s(e.personalInfo.ID))])])],1),n("a-col",{attrs:{span:8}},[n("detail-list-item",{staticClass:"item_label",attrs:{term:"电话"}},[n("span",{staticClass:"info_display"},[e._v(e._s(e.personalInfo.phone_number))])])],1),n("a-col",{attrs:{span:5}},[n("detail-list-item",{staticClass:"item_label",attrs:{term:"医疗站点编号"}},[n("span",{staticClass:"info_display"},[e._v(e._s(e.personalInfo.hos_id))])])],1)],1)],1),n("a-divider",{staticStyle:{"margin-bottom":"32px"}}),n("detail-list",[n("h1",{staticClass:"title"},[e._v("健康")]),n("a-row",[n("detail-list-item",{staticClass:"item_label",attrs:{term:"健康状态"}},[n("span",{staticClass:"info_display"},[e._v(e._s(e.health))])])],1)],1)],1)],1)},s=[],a=(n("b0c0"),n("ac1f"),n("1276"),n("cebe")),r=n.n(a),o=n("ed3b"),l=n("c340"),d=n("45eb"),c=n("8bbf"),u=n.n(c),h=n("ec7e"),f=n.n(h),p=n("7424"),m=n("b775"),b=n("be65"),v=n.n(b);u.a.use(f.a);var _=l["a"].Item,g={name:"BasicDetail",components:{PageLayout:d["a"],DetailListItem:_,DetailList:l["a"]},data:function(){return{personalInfo:{phone_number:"123",ID:"123",hos_id:"123"},edited:{name:"",gender:"",age:"",phone_number:"",ID:"",address:""},user:null,health:null,editMode:!1,Authorization:"123",DeliveredUserInfo:[],DeliveredUserHealthStatus:[]}},created:function(){this.fetchUserInfo(),this.getHealthInfo()},methods:{toggleEditMode:function(){this.editMode?""==this.edited.name||""==this.edited.gender||""==this.edited.phone_number||""==this.edited.address?(o["a"].warning({title:"警告",content:"请填写完整信息（不能有空）！"}),this.cancelEditMode()):(this.personalInfo.name=this.edited.name,this.personalInfo.gender=this.edited.gender,this.personalInfo.phone_number=this.edited.phone_number,this.personalInfo.address=this.edited.address,this.editMode=!1,this.sendUserInfo(this.edited,this.Authorization)):(this.edited.name=this.personalInfo.name,this.edited.gender=this.personalInfo.gender,this.edited.phone_number=this.personalInfo.phone_number,this.edited.address=this.personalInfo.address,this.editMode=!0)},cancelEditMode:function(){this.editMode=!1,this.edited.name="",this.edited.gender="",this.edited.age="",this.edited.phone_number="",this.edited.ID="",this.edited.address=""},fetchUserInfo:function(){var e=localStorage.getItem("admin.user");this.user=JSON.parse(e);var t=localStorage.getItem("admin.roles");this.roles=JSON.parse(t),this.personalInfo.ID=this.user.id,this.personalInfo.hos_id=this.user.hos_id,this.personalInfo.phone_number=this.user.phone},getHealthInfo:function(){var e=this;r.a.get(p["ADDRESS"]+"/MedicalStaff/GetWorkerHealth",{headers:{Authorization:this.Authorization}}).then((function(t){e.health=t.data.worker_state})).catch((function(e){}))},getAuthorization:function(){this.Authorization=v.a.get(m["xsrfHeaderName"]).Authorization.split(" ")[1]}}},I=g,y=(n("7170"),n("0c7c")),w=Object(y["a"])(I,i,s,!1,null,"eb6afd90",null);t["default"]=w.exports},"42a1":function(e,t,n){},6531:function(e,t,n){"use strict";var i=n("42a1"),s=n.n(i);s.a},7170:function(e,t,n){"use strict";var i=n("9071"),s=n.n(i);s.a},9071:function(e,t,n){},c340:function(e,t,n){"use strict";var i=function(){var e=this,t=e.$createElement,n=e._self._c||t;return n("div",{class:["detail-list","small"===e.size?"small":"large","vertical"===e.layout?"vertical":"horizontal"]},[e.title?n("div",{staticClass:"title"},[e._v(e._s(e.title))]):e._e(),n("a-row",[e._t("default")],2)],1)},s=[],a=(n("a9e3"),n("da05")),r={name:"DetailListItem",props:{term:{type:String,required:!1}},inject:{col:{type:Number}},methods:{renderTerm:function(e,t){return t?e("div",{attrs:{class:"term"}},[t]):null},renderContent:function(e,t){return e("div",{attrs:{class:"content"}},[t])}},render:function(e){var t=this.renderTerm(e,this.$props.term),n=this.renderContent(e,this.$slots.default);return e(a["b"],{props:o[this.col]},[t,n])}},o={1:{xs:24},2:{xs:24,sm:12},3:{xs:24,sm:12,md:8},4:{xs:24,sm:12,md:6}},l={name:"DetailList",Item:r,props:{title:{type:String,required:!1},col:{type:Number,required:!1,default:3},size:{type:String,required:!1,default:"large"},layout:{type:String,required:!1,default:"horizontal"}},provide:function(){return{col:this.col>4?4:this.col}}},d=l,c=(n("6531"),n("0c7c")),u=Object(c["a"])(d,i,s,!1,null,null,null);t["a"]=u.exports},ec7e:function(e,t,n){var i,s,a;(function(n,r){s=[],i=r,a="function"===typeof i?i.apply(t,s):i,void 0===a||(e.exports=a)})(0,(function(){
/**
 * Vue Jsonp By LancerComet at 16:35, 2016.10.17.
 * # Carry Your World #
 *
 * @author: LancerComet
 * @license: MIT
 */
var e=null,t={install:function(t,i){t.jsonp=n,t.prototype.$jsonp=n,"number"===typeof i&&(e=i)}};function n(t,n,r){return n=n||{},r=r||e,new Promise((function(e,o){if("string"!==typeof t)throw new Error('[Vue.jsonp] Type of param "url" is not string.');var l=n.callbackQuery||"callback",d=n.callbackName||"jsonp_"+i();n[l]=d,delete n.callbackQuery,delete n.callbackName;var c=[];Object.keys(n).forEach((function(e){c=c.concat(s(e,n[e]))}));var u=a(c).join("&"),h=null;"number"===typeof r&&(h=setTimeout((function(){b(),f.removeChild(p),delete window[d],o({statusText:"Request Timeout",status:408})}),r)),window[d]=function(t){clearTimeout(h),b(),f.removeChild(p),e(t),delete window[d]};var f=document.querySelector("head"),p=document.createElement("script");function m(e){b(),clearTimeout(h),o({status:400,statusText:"Bad Request"})}function b(){p.removeEventListener("error",m)}p.addEventListener("error",m),p.src=t+(/\?/.test(t)?"&":"?")+u,f.appendChild(p)}))}function i(){return(Math.floor(1e5*Math.random())*Date.now()).toString(16)}function s(e,t){e=e.replace(/=/g,"");var n=[];switch(t.constructor){case String:case Number:case Boolean:n.push(encodeURIComponent(e)+"="+encodeURIComponent(t));break;case Array:t.forEach((function(t){n=n.concat(s(e+"[]=",t))}));break;case Object:Object.keys(t).forEach((function(i){var a=t[i];n=n.concat(s(e+"["+i+"]",a))}));break}return n}function a(e){var t=[];return e.forEach((function(e){"string"===typeof e?t.push(e):t=t.concat(a(e))})),t}return t}))}}]);