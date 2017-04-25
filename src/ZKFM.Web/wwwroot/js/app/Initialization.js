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

    //歌曲列表按钮
    listStatus = 0;
    $('.list-button').click(function () {
        listWidth = $('.playlist').width();
        if (!listStatus) {
            $(this).animate({ marginLeft: listWidth + 30 }, 300);
            $('.playlist').animate({ marginLeft: 0 }, 300);
            listStatus = 1;
        } else {
            $(this).animate({ marginLeft: 10 }, 300);
            $('.playlist').animate({ marginLeft: -500 }, 300);
            listStatus = 0;
        }
    });
    //搜索列表
    SearchlistStatus = 0;
    $('.searchbtn').click(function () {
        if (!SearchlistStatus) {
            //$(this).animate({ marginLeft: listWidth + 30 }, 300);
            $('.searchbox').animate({ top: 0 }, 500);
            SearchlistStatus = 1;
        } else {
            //$(this).animate({ marginLeft: 10 }, 300);
            $('.searchbox').animate({ top: -660 }, 500);
            SearchlistStatus = 0;
        }
    });
    //GitHub
    $('.github').click(function () {
        window.open('https://github.com/niubileme/ZKFM');
    });
    //循环方式
    $('.repeat').click(function () {
        var r = $(".repeat").attr("r");
        if (r == 0) {
            $(".repeat").attr("r", "1");
            $(".repeat i").attr("title", "列表播放").removeClass("fa-random").addClass("fa-refresh");
            audio.loop = true;
        } else {
            $(".repeat").attr("r", "0");
            $(".repeat i").attr("title", "单曲循环").removeClass("fa-refresh").addClass("fa-random");
            audio.loop = false;
        }
    });
    //下一首
    $('.nexts').click(function () {
        $(".repeat").attr("r", "0");
        $(".repeat i").attr("title", "单曲循环");
        audio.loop = false;
        loadMusic();
    });
    //关于
    $('.about').click(function () {
        alert("关于说明：\n歌曲自定义上传,编辑,歌词同步,\n登陆,收藏,下载,收听好友歌曲等社交功能...\n\n然而这并没有什么卵用...有啥问题,欢迎留言~");
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
