define(['jquery', 'logger', 'player'], function ($, logger, player) {

    function getIndex(id) {
        for (var j = 0; j < musicList.length; j++) {
            if (musicList[j].id === id) {
                return j;
            }
        }
        return 0;
    };

    var addMusic = function (o) {
        logger.debug(o);
        musicList.push(o);
        $('<div class="playlist-item" m="' + o.id + '"><div class="singleinfo">' + o.name + '</div><div class="deletesong"><i class="delete fa fa-trash" title="删除"></i></div></div>').appendTo('#result');
        if (window.localStorage) {
            localStorage.setItem(o.id, o.name);
        }
    };

    var addMusic2 = function (id, name) {
        var isContain = false;
        for (var i = 0; i < musicList.length; i++) {
            if (id == musicList[i].id) {
                isContain = true;
                break;
            }
        }
        if (!isContain) {
            this.addMusic({ id: id, name: name });
        }
        this.playMusic(id);
    };

    var delMusic = function (id) {
        for (var i = 0; i < musiclist.length; i++) {
            if (id == musiclist[i].id) {
                musiclist.splice(i, 1);
                break;
            }
        }
        $('#result').children('[m="' + id + '"]').remove();
        if (window.localStorage) {
            localStorage.removeItem(id);
        }
        if (id == $('.musicid').text()) {
            this.LoadMusic();
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
                    player.SetLoop(false);
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
                    player.SetAudioSrc(data.src);
                    player.Play();
                } else {
                    logger.info(response.msg);
                }
            },
            error: function (e) {
                logger.error("playMusic error:" + e);
            }
        });
    };

    var LoadMusic = function () {
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
            this.playMusic(id);
        }
    };

    var Search = function () {
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
        addMusic: addMusic,
        addMusic2: addMusic2,
        delMusic: delMusic,
        playMusic: playMusic,
        LoadMusic: LoadMusic,
        Search: Search
    };

});