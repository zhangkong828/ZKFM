//初始化相关
$(document).ready(function () {
    //浏览器版本
    if ((navigator.appName == "Microsoft Internet Explorer" && navigator.appVersion.match(/6./i) == "6.") || (navigator.appName == "Microsoft Internet Explorer" && navigator.appVersion.match(/7./i) == "7.") || (navigator.appName == "Microsoft Internet Explorer" && navigator.appVersion.match(/8./i) == "8.") || (navigator.appName == "Microsoft Internet Explorer" && navigator.appVersion.match(/9./i) == "9.")) {
        window.location.href = "noie.html";
    }

    initUI();
    
    $(window).resize(function () {
        console.log("window resize");
        initUI();
    });


});

//初始化UI
function initUI() {
    var w = $(window).width();
    var h = $(window).height();
    //搜索box
    var searchbox = $(".searchbox");
    searchbox.css("left", ((w - searchbox.width()) / 2));
    //搜索按钮
    var searchbtn = $(".searchbtn");
    searchbtn.css("left", searchbox.position().left + searchbox.width());
    //封面
    var pic = $("#pic");
    pic.css("top", ((h - pic.height()) / 2)).css("left", ((w - pic.width()) / 2));
    //封面中间空白
    var center = $(".center");
    center.css("top", ((h - center.height()) / 2)).css("left", ((w - center.width()) / 2));
    //封面中的播放/暂停按钮
    var start = $(".start");
    start.css("top", ((h - start.height()) / 2)).css("left", ((w - start.width()) / 2));
    //歌曲信息
    var title = $(".title");
    title.css("top", (h - pic.height() - title.height() - 30) / 2 + pic.height() + title.height() - 10).css("left", ((w - title.width()) / 2));
};
