(window["webpackJsonp"]=window["webpackJsonp"]||[]).push([["chunk-6165aaaa"],{"29fc":function(t,e,a){"use strict";a.r(e);var r=function(){var t=this,e=t.$createElement,a=t._self._c||e;return a("page-layout",{attrs:{title:"工作安排信息页"}},[a("a-card",{attrs:{bordered:!1}},[a("detail-list",{attrs:{title:"疫苗管理"}},[a("button",[t._v("疫苗申请")]),a("button",[t._v("数据统计")])]),a("a-divider",{staticStyle:{"margin-bottom":"32px"}}),a("div",{staticClass:"title"},[t._v("消杀安排")]),a("a-table",{staticStyle:{"margin-bottom":"24px"},attrs:{"row-key":"id",columns:t.workColumns,dataSource:t.workData,pagination:!1}}),a("div",{staticClass:"title"},[t._v("核酸信息上传")]),a("button",[t._v("核酸信息上传")])],1)],1)},i=[],n=a("c340"),l=a("45eb"),s=[{title:"消杀地点",dataIndex:"place",key:"place"},{title:"消杀时间段",dataIndex:"time",key:"time"}],o=[{place:"A街道",time:"14:30-15:30"},{place:"B街道",time:"16:30-18:30"},{place:"C街道",time:"19:30-20:30"}],c={name:"BasicDetail",components:{PageLayout:l["a"],DetailList:n["a"]},data:function(){return{workColumns:s,workData:o}}},u=c,d=(a("4d07"),a("0c7c")),m=Object(d["a"])(u,r,i,!1,null,"571eb4f8",null);e["default"]=m.exports},"39b9":function(t,e,a){},"42a1":function(t,e,a){},"4d07":function(t,e,a){"use strict";var r=a("39b9"),i=a.n(r);i.a},6531:function(t,e,a){"use strict";var r=a("42a1"),i=a.n(r);i.a},c340:function(t,e,a){"use strict";var r=function(){var t=this,e=t.$createElement,a=t._self._c||e;return a("div",{class:["detail-list","small"===t.size?"small":"large","vertical"===t.layout?"vertical":"horizontal"]},[t.title?a("div",{staticClass:"title"},[t._v(t._s(t.title))]):t._e(),a("a-row",[t._t("default")],2)],1)},i=[],n=(a("a9e3"),a("da05")),l={name:"DetailListItem",props:{term:{type:String,required:!1}},inject:{col:{type:Number}},methods:{renderTerm:function(t,e){return e?t("div",{attrs:{class:"term"}},[e]):null},renderContent:function(t,e){return t("div",{attrs:{class:"content"}},[e])}},render:function(t){var e=this.renderTerm(t,this.$props.term),a=this.renderContent(t,this.$slots.default);return t(n["b"],{props:s[this.col]},[e,a])}},s={1:{xs:24},2:{xs:24,sm:12},3:{xs:24,sm:12,md:8},4:{xs:24,sm:12,md:6}},o={name:"DetailList",Item:l,props:{title:{type:String,required:!1},col:{type:Number,required:!1,default:3},size:{type:String,required:!1,default:"large"},layout:{type:String,required:!1,default:"horizontal"}},provide:function(){return{col:this.col>4?4:this.col}}},c=o,u=(a("6531"),a("0c7c")),d=Object(u["a"])(c,r,i,!1,null,null,null);e["a"]=d.exports}}]);