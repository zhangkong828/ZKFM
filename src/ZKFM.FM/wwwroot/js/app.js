requirejs.config({
    baseUrl: 'js/lib',
    paths: {
        jquery: '../jquery.min',
        lib: '../lib'
    }
});

requirejs(['jquery', 'lib/core', 'lib/controller', 'lib/config', 'lib/logger'], function ($, core, controller, config, logger) {

    musicList = [];//播放列表
    currentIndex = -1;
    timeout = 0;
    lrctimeout = 0;

    var initUI = function () {
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

    $(window).resize(function () {
        logger.debug("window resize");
        initUI();
    });

    initUI();

    if (window.localStorage) {
        var storage = window.localStorage;
        for (var i = 0; i < storage.length; i++) {
            var key = storage.key(i);
            if (!isNaN(Number(key))) {
                controller.AddMusic({ id: storage.key(i), name: storage.getItem(storage.key(i)) });
            }
        }
    }
    //本地无数据 加载默认配置
    if (musicList.length == 0) {
        $.each(config.default.music, function (i, v) {
            controller.AddMusic({ id: v.id, name: v.name });
        });
    }

    core.LoadMusic();

    $('.start i').click(function () {
        if ($(this).hasClass('playing')) {
            core.Pause();
        } else {
            core.Play();
        }
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
            $('.searchbox').animate({ top: 0 }, 500);
            SearchlistStatus = 1;
        } else {
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
            core.SetLoop(true);
        } else {
            $(".repeat").attr("r", "0");
            $(".repeat i").attr("title", "单曲循环").removeClass("fa-refresh").addClass("fa-random");
            core.SetLoop(false);
        }
    });
    //下一首
    $('.nexts').click(function () {
        core.LoadMusic();
    });
    //关于
    $('.about').click(function () {
        alert("关于说明：\n有啥问题,欢迎fork，提Issuee");
    });


    $(document).on("click", "#searchresult p", function () {
        var id = $(this).attr("m");
        var name = $(this).text();
        controller.AddMusic2(id, name);
        core.PlayMusic(id);
    });
    $(document).on("click", ".delete", function () {
        var id = $(this).parent().parent().attr("m");
        controller.DelMusic(id);
        if (id == $('.musicid').text()) {
            core.LoadMusic();
        }
    });
    $(document).on("click", ".singleinfo", function () {
        var id = $(this).parent().attr("m");
        core.PlayMusic(id);
    });

    //搜索
    $("#navi").click(function () {
        var page = parseInt($("#page").val());
        $("#page").val(++page);
        core.SearchMusic();
    });
    $("body").keydown(function (e) {
        if (e.keyCode == "13") {//回车
            if ($('#text').val() != '') {
                $('#searchresult').html('');
                core.SearchMusic();
            }
        }
    });
});