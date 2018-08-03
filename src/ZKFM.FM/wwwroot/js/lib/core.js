define(['jquery', 'lib/logger'], function ($, logger) {
    audio = document.getElementById("music");

    function getIndex(id) {
        for (var j = 0; j < musicList.length; j++) {
            if (musicList[j].id === id) {
                return j;
            }
        }
        return 0;
    };

    function Lyc() {
        console.log("lyc")
    }
    //歌词
    var lrcMove = function () {
        //if (lrc_index.length > 0) {
        //    var time = parseInt(audio.currentTime);
        //    //console.log(time);
        //    var i = 0;
        //    for (var j = 0; j < lrc_index.length; j++) {
        //        if (lrc_index[j].time == time) {
        //            i = lrc_index[j].index;
        //            break;
        //        }
        //    }
        //    if (i != 0) {
        //        var y = i * 30;
        //        // console.log("line:" + i);
        //        $("#lrc-lines p").removeClass("lrc-current").eq(i - 1).addClass("lrc-current");
        //        $("#lrc-lines").animate({ marginTop: (30 - y) + "px" });
        //    }
        //}
    };

    //进度条
    var updateProgress = function () {
        if (audio.currentTime == audio.duration) {
            clearInterval(timeout);
            clearInterval(lrctimeout);
            ratio = 0;
            $('#wrap .progress .current').css({ 'width': ratio + '%' });
            loadMusic();
        }
        ratio = audio.currentTime / audio.duration * 100;
        $('#wrap .progress .current').css({ 'width': ratio + '%' });
        logger.info(ratio);
    };

    var play = function () {
        audio.play();
        $('.album').addClass('playing');
        $('.start i').addClass('playing').removeClass('fa-play').addClass('fa-pause');
        timeout = setInterval(updateProgress, 1000);
        lrctimeout = setInterval(lrcMove, 1000);
        $('#pic').css("animationPlayState", "running");
    };

    var pause = function () {
        audio.pause();
        $('.album').removeClass('playing');
        $('.start i').removeClass('playing').removeClass('fa-pause').addClass('fa-play');
        clearInterval(timeout);
        clearInterval(lrctimeout);
        $('#pic').css("animationPlayState", "paused");
    };

    var getLoop = function () {
        return audio.loop;
    };

    var setLoop = function (isloop) {
        audio.loop = isloop;
    };

    var setAudioSrc = function (src) {
        audio.src = src;
    };

    var loadMusic = function () {
        var r = $(".repeat").attr("r");
        if (r == 0) {
            if (musicList.length <= 0) {
                logger.info("播放列表为空");
                return;
            }
            currentIndex++;
            if (currentIndex >= musicList.length) {
                currentIndex = 0;
            }
            var id = musicList[currentIndex].id;
            playMusic(id);
        }
    };

    var playMusic = function (id) {
        logger.debug("play:" + id);
        currentIndex = getIndex(id);
        $("#lrc-lines").css(" margin-top", "0px").html('');
        clearInterval(timeout);
        clearInterval(lrctimeout);
        $('#wrap .progress .current').css({ 'width': '0%' });
        $.ajax({
            type: "get",
            url: "/API/NetEaseMusic/V1/Get/" + id,
            success: function (response) {
                logger.debug(response);
                if (response.code == 0) {
                    var data = response.data;
                    $(".repeat").attr("r", "0");
                    $(".repeat i").attr("title", "单曲循环");
                    setLoop(false);
                    if (data.pic != null) {
                        $('.album img').attr('src', data.pic);
                    } else {
                        $('.album img').attr('src', '../img/album.jpg');
                    }
                    $('.musicid').text(data.id);
                    $('.album img').attr('alt', data.name);
                    $(".name").html(data.name);
                    $(".sub-title").html(data.author);
                    $('.singleinfo').removeClass('active');
                    $('.playlist-item[m="' + id + '"]').children('.singleinfo').addClass('active');
                    //$("#lrc-lines").html(data.lrc_lines);
                    //lrc_index = data.lrc_index;
                    setAudioSrc(data.src);
                    play();
                } else {
                    logger.info(response.msg);
                }
            },
            error: function (e) {
                logger.error("playMusic error:" + e);
            }
        });
    };

    var searchMusic = function () {
        $.ajax({
            url: "API/NetEaseMusic/V1/Search/",
            type: "post",
            data: { key: encodeURIComponent($('#text').val()), index: $('#page').val() },
            success: function (response) {
                logger.debug(response);
                if (response.code == 0) {
                    var data = response.data;
                    $("#searchlist").css('display', 'block');
                    if (data.total / 8 != 1) {
                        $('#navi').html('<div class="more">载入更多</div>');
                    }
                    $.each(data.datas, function (i, item) {
                        $('<p m="' + item.id + '">' + decodeURIComponent(item.name) + ' - ' + decodeURIComponent
                            (item.author) + '</p>').appendTo('#searchresult');
                    });
                }
            },
            error: function (e) {
                logger.error("Search error:" + e);
                $('#searchlist').css('display', 'none');
                $('#searchresult').html(e);
            }
        });
    };

    return {
        Play: play,
        Pause: pause,
        GetLoop: getLoop,
        SetLoop: setLoop,
        SetAudioSrc: setAudioSrc,
        LoadMusic: loadMusic,
        PlayMusic: playMusic,
        SearchMusic: searchMusic
    };
});