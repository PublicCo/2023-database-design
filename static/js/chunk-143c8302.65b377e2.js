(window["webpackJsonp"]=window["webpackJsonp"]||[]).push([["chunk-143c8302"],{1233:function(t,i,n){},"236b":function(t,i,n){"use strict";var a=n("1233"),e=n.n(a);e.a},"47ef":function(t,i,n){"use strict";n.r(i);var a=function(){var t=this,i=t.$createElement,n=t._self._c||i;return n("div",[n("div",{staticClass:"chart-container",attrs:{loading:t.loading,id:"myChart"}}),n("div",{staticClass:"chart-container",attrs:{loading:t.loading,id:"lineChart"}})])},e=[],r=n("313e"),s={name:"hello",data:function(){return{loading:!0}},created:function(){var t=this;setTimeout((function(){t.loading=!1,t.initChart(),t.initLineChart()}),100)},mounted:function(){this.drawLine(),this.drawLineChart()},methods:{initChart:function(){this.myChart=r.init(document.getElementById("myChart")),this.drawLine()},initLineChart:function(){this.lineChart=r.init(document.getElementById("lineChart")),this.drawLineChart()},drawLine:function(){this.myChart&&this.myChart.setOption({title:{text:"上海市感染者新增"},tooltip:{},xAxis:{data:["虹口","嘉定","宝山","杨浦","松江","黄浦","徐汇","静安","长宁","普陀"]},yAxis:{},series:[{name:"当日新增感染者",type:"bar",data:[5,20,36,10,10,20,100,1,0,20]}]})},drawLineChart:function(){this.lineChart&&this.lineChart.setOption({title:{text:"上海市感染者趋势"},tooltip:{},xAxis:{data:["1月","2月","3月","4月","5月","6月","7月","8月","9月","10月"]},yAxis:{},series:[{name:"感染趋势",type:"line",data:[100,150,200,180,250,300,280,320,350,400]}]})}}},o=s,h=(n("236b"),n("0c7c")),c=Object(h["a"])(o,a,e,!1,null,null,null),d=c.exports;i["default"]=d}}]);