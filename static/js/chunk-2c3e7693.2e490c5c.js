(window["webpackJsonp"]=window["webpackJsonp"]||[]).push([["chunk-2c3e7693"],{"253f":function(t,e,i){},"42a1":function(t,e,i){},6531:function(t,e,i){"use strict";var a=i("42a1"),n=i.n(a);n.a},8967:function(t,e,i){"use strict";var a=i("253f"),n=i.n(a);n.a},"9ed0":function(t,e,i){"use strict";i.r(e);var a=function(){var t=this,e=t.$createElement,a=t._self._c||e;return a("div",[a("a-card",{attrs:{"body-style":{padding:"24px 32px"},bordered:!1}},[a("h1",{staticClass:"title"},[t._v("撰写公告 ")]),a("a-form",[a("a-form-item",{attrs:{label:"标题",labelCol:{span:7},wrapperCol:{span:10}}},[a("a-input",{attrs:{placeholder:"请输入标题"},model:{value:t.name,callback:function(e){t.name=e},expression:"name"}})],1),a("a-form-item",{attrs:{label:"系统时间",labelCol:{span:7},wrapperCol:{span:10}}},[a("p",[t._v(t._s(t.time))])]),a("a-form-item",{attrs:{label:"公告内容",labelCol:{span:7},wrapperCol:{span:10}}},[a("a-textarea",{attrs:{rows:10,placeholder:"1000字以内","max-length":1e3},model:{value:t.content,callback:function(e){t.content=e},expression:"content"}})],1),a("a-form-item",{attrs:{label:"公开：",labelCol:{span:7},wrapperCol:{span:10},required:!1,help:"所有均公开"}},[a("a-radio-group",[a("a-radio",[t._v(t._s("公开"))])],1)],1),a("a-form-item",{staticStyle:{"margin-top":"24px"},attrs:{wrapperCol:{span:10,offset:7}}},[a("a-button",{attrs:{type:"primary"},on:{click:t.clickSubmit}},[t._v(t._s("提交"))])],1)],1)],1),a("a-card",{staticStyle:{"margin-top":"24px"},attrs:{bordered:!1,title:"公告修改"}},[t._l(t.currentData,(function(e){return a("div",{key:e.index},[a("detail-list",[a("a-row",[a("a-col",{attrs:{span:5}},[a("a-list-item-meta",{attrs:{description:e.name}},[a("a-avatar",{attrs:{slot:"avatar",size:"large",shape:"square",src:i("a656")},slot:"avatar"}),a("a",{attrs:{slot:"title"},slot:"title"},[t._v(t._s(e.title?e.title:"公告标题"))])],1)],1),a("a-col",{attrs:{span:"5"}},[a("div",{staticClass:"list-content-item"},[a("span",[t._v("编号")]),a("p",{staticClass:"content-text"},[t._v(t._s(e.notice_id))])])]),a("a-col",{attrs:{span:"6"}},[a("div",{staticClass:"list-content-item"},[a("span",[t._v("内容：")]),a("a-button",{attrs:{type:"primary"},on:{click:function(i){return t.showContent(e.name,e.content)}}},[t._v(t._s("点击显示"))])],1)]),a("a-modal",{attrs:{title:"公告内容"},model:{value:t.showContentModal,callback:function(e){t.showContentModal=e},expression:"showContentModal"}},[a("p",[a("strong",[t._v("标题："+t._s(t.currentTitle))])]),a("p",[t._v("内容："+t._s(t.currentContent))])]),a("a-col",{attrs:{span:"4"}},[a("a-button",{attrs:{type:"primary"},on:{click:function(i){return t.modifyNotice(e.name,e.notice_id,e.content)}}},[t._v(t._s("修改"))])],1),a("a-modal",{attrs:{title:"修改公告"},model:{value:t.showModifyModal,callback:function(e){t.showModifyModal=e},expression:"showModifyModal"}},[a("a-form",[a("a-form-item",{attrs:{label:"标题"}},[a("a-input",{model:{value:t.modifiedTitle,callback:function(e){t.modifiedTitle=e},expression:"modifiedTitle"}})],1),a("P",[t._v("编号："+t._s(t.modifiedNotice_ID))]),a("a-form-item",{attrs:{label:"内容"}},[a("a-textarea",{attrs:{rows:6},model:{value:t.modifiedContent,callback:function(e){t.modifiedContent=e},expression:"modifiedContent"}})],1)],1),a("div",{attrs:{slot:"footer"},slot:"footer"},[a("a-button",{on:{click:t.cancelModify}},[t._v("取消")]),a("a-button",{attrs:{type:"primary"},on:{click:t.saveModify}},[t._v("保存")])],1)],1)],1)],1),a("a-divider")],1)})),a("a-row",{attrs:{type:"flex",align:"middle"}},[a("a-col",{attrs:{span:8}},[a("a-pagination",{attrs:{total:t.HistoryNotice.length,current:t.currentPage,"page-size":t.pageSize},on:{change:t.handlePageChange}})],1),a("a-col",{attrs:{span:2}},[a("p"),a("a-dropdown",[a("a-menu",{attrs:{slot:"overlay"},slot:"overlay"},[a("a-menu-item",{on:{click:function(e){return t.changePageSize(1)}}},[t._v(" 1 条/每页")]),a("a-menu-item",{on:{click:function(e){return t.changePageSize(2)}}},[t._v(" 2 条/每页")]),a("a-menu-item",{on:{click:function(e){return t.changePageSize(5)}}},[t._v(" 5 条/每页")]),a("a-menu-item",{on:{click:function(e){return t.changePageSize(10)}}},[t._v(" 10 条/每页")]),a("a-menu-item",{on:{click:function(e){return t.changePageSize(20)}}},[t._v(" 20 条/每页")])],1),a("a",[t._v(t._s(t.pageSize)+"条/每页"),a("a-icon",{attrs:{type:"down"}})],1)],1)],1),a("a-col",{attrs:{span:8}},[a("span",[t._v("跳转到第")]),a("a-input-number",{model:{value:t.jumpPage,callback:function(e){t.jumpPage=e},expression:"jumpPage"}}),a("span",[t._v("页")]),a("a-button",{attrs:{type:"default"},on:{click:t.goToPage}},[t._v("跳转")])],1)],1)],2)],1)},n=[],o=(i("99af"),i("fb6a"),i("b0c0"),i("ac1f"),i("1276"),i("ed3b")),s=i("cebe"),r=i.n(s),c=i("c340"),l=i("7424"),u=i("be65"),d=i.n(u),h=i("b775"),m={name:"BasicForm",components:{DetailList:c["a"]},data:function(){return{value:1,name:"",content:"",time:"",Authorization:"123",showContentModal:!1,HistoryNotice:[],DeliveredNotice:[],currentTitle:"",currentContent:"",showModifyModal:!1,modifiedTitle:"",modifiedNotice_ID:"",modifiedContent:"",currentPage:1,pageSize:5,jumpPage:1}},computed:{currentData:function(){var t=(this.currentPage-1)*this.pageSize,e=t+this.pageSize;return this.HistoryNotice.slice(t,e)}},created:function(){this.updateCurrentTime(),this.fetchNotice()},methods:{getLongitudeLatitudeAndTime:function(){var t=new Date,e=t.getFullYear(),i=t.getMonth()+1,a=t.getDate(),n=t.getHours(),o=t.getMinutes(),s=t.getSeconds();return this.time="".concat(e,"/").concat(i,"/").concat(a," ").concat(n,":").concat(o,":").concat(s),this.time},updateCurrentTime:function(){var t=this;this.getLongitudeLatitudeAndTime(),setInterval((function(){t.getLongitudeLatitudeAndTime()}),1e3)},clickSubmit:function(){""==this.name||""==this.content?o["a"].warning({title:"警告",content:"标题和内容不得为空！"}):(this.sendNotice(this.Authorization),this.name="",this.content="")},sendNotice:function(t){this.getAuthorization();this.name,this.content,this.time;r.a.post(l["ADDRESS"]+"/Notice/CreateNotice",{},{headers:{Authorization:this.Authorization},params:{name:this.name,content:this.content,time:this.time}}).then((function(t){200==t.status&&o["a"].success({title:"成功",content:"提交成功！"})})).catch((function(t){}))},fetchNotice:function(){var t=this;this.getAuthorization(),r.a.get(l["ADDRESS"]+"/Notice/GetNotice",{headers:{Authorization:this.Authorization}}).then((function(e){"200"==e.data.code&&(t.DeliveredNotice=e.data,t.HistoryNotice=t.DeliveredNotice.MessageDetail)})).catch((function(t){}))},changePageSize:function(t){this.pageSize=t,this.currentPage=1},goToPage:function(){this.jumpPage>=1&&this.jumpPage<=Math.ceil(this.vaccinationRequest.taskNum/this.pageSize)?this.currentPage=this.jumpPage:(this.jumpPage=this.currentPage,o["a"].warning({title:"警告",content:"输入了非法页数！"}))},handlePageChange:function(t){this.currentPage=t},showContent:function(t,e){this.currentTitle=t,this.currentContent=e,this.showContentModal=!0},modifyNotice:function(t,e,i){this.modifiedTitle=t,this.modifiedNotice_ID=e,this.modifiedContent=i,this.showModifyModal=!0},saveModify:function(){this.showModifyModal=!1,this.sendModifyNotice(this.Authorization),this.fetchNotice()},cancelModify:function(){this.showModifyModal=!1},sendModifyNotice:function(t){this.getAuthorization();this.modifiedTitle,this.modifiedContent,this.modifiedNotice_ID,this.time;r.a.post(l["ADDRESS"]+"/Notice/UpdateNotice",{},{headers:{Authorization:this.Authorization},params:{notice_id:this.modifiedNotice_ID,notice_name:this.modifiedTitle,content:this.modifiedContent,time:this.time}}).then((function(t){200==t.status&&o["a"].success({title:"成功",content:"提交成功！"})})).catch((function(t){}))},getAuthorization:function(){this.Authorization=d.a.get(h["default"]).Authorization.split(" ")[1]}}},p=m,f=(i("8967"),i("0c7c")),g=Object(f["a"])(p,a,n,!1,null,"1a80bd1f",null);e["default"]=g.exports},a656:function(t,e,i){t.exports=i.p+"static/img/hos_logo.428b9810.png"},c340:function(t,e,i){"use strict";var a=function(){var t=this,e=t.$createElement,i=t._self._c||e;return i("div",{class:["detail-list","small"===t.size?"small":"large","vertical"===t.layout?"vertical":"horizontal"]},[t.title?i("div",{staticClass:"title"},[t._v(t._s(t.title))]):t._e(),i("a-row",[t._t("default")],2)],1)},n=[],o=(i("a9e3"),i("da05")),s={name:"DetailListItem",props:{term:{type:String,required:!1}},inject:{col:{type:Number}},methods:{renderTerm:function(t,e){return e?t("div",{attrs:{class:"term"}},[e]):null},renderContent:function(t,e){return t("div",{attrs:{class:"content"}},[e])}},render:function(t){var e=this.renderTerm(t,this.$props.term),i=this.renderContent(t,this.$slots.default);return t(o["b"],{props:r[this.col]},[e,i])}},r={1:{xs:24},2:{xs:24,sm:12},3:{xs:24,sm:12,md:8},4:{xs:24,sm:12,md:6}},c={name:"DetailList",Item:s,props:{title:{type:String,required:!1},col:{type:Number,required:!1,default:3},size:{type:String,required:!1,default:"large"},layout:{type:String,required:!1,default:"horizontal"}},provide:function(){return{col:this.col>4?4:this.col}}},l=c,u=(i("6531"),i("0c7c")),d=Object(u["a"])(l,a,n,!1,null,null,null);e["a"]=d.exports}}]);