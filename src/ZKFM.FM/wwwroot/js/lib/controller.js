define(['jquery', 'logger', 'config'], function ($, logger, config) {
    return {
        addMusic: function (o) {
            logger.debug(o);
            musicList.push(o);
            $('<div class="playlist-item" m="' + o.id + '"><div class="singleinfo">' + o.name + '</div><div class="deletesong"><i class="delete fa fa-trash" title="删除"></i></div></div>').appendTo('#result');
            if (window.localStorage) {
                localStorage.setItem(o.id, o.name);
            }
        },
        addMusic2: function (id, name) {
            var isContain = false;
            for (var i = 0; i < musicList.length; i++) {
                if (id == musicList[i].id) {
                    isContain = true;
                    break;
                }
            }
            if (!isContain) {
                addMusic({ id: id, name: name });
            }
            playMusic(id);
        },
        delMusic: function (id) {
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
            //if (id == $('.xiamiid').text()) {
            //    loadMusic();
            //}
        },
        playMusic: function (id) {
            logger.debug("play:" + id);
            //cur = getIndex(id);
            $("#lrc-lines").css(" margin-top", "0px").html('');
            clearInterval(timeout);
            clearInterval(lrctimeout);
            $('#wrap .progress .current').css({ 'width': '0%' });
            $.ajax({
                type: "post",
                url: "/search/song?id=" + id,
                cache: false,
                success: function (data) {
                    logger.debug(data);
                    if (data != "error" && data.src.substr(0, 4) == 'http') {
                        $(".repeat").attr("r", "0");
                        $(".repeat i").attr("title", "单曲循环");
                        audio.loop = false;
                        if (data.pic != null) {
                            $('.album img').attr('src', data.pic);
                        } else {
                            $('.album img').attr('src', '../img/album.jpg');
                        }
                        audio.setAttribute("src", data.src);
                        $('.xiamiid').text(id);
                        $('.album img').attr('alt', data.name);
                        $(".name").html(data.name);
                        $(".sub-title").html(data.author);
                        $('.singleinfo').removeClass('active');
                        $('.playlist-item[m="' + id + '"]').children('.singleinfo').addClass('active');
                        $("#lrc-lines").html(data.lrc_lines);
                        lrc_index = data.lrc_index;
                        play();
                    } else {
                        //console.log("网络似乎出了点小问题，暂时没有找到！换下一首吧");
                        setTimeout(function () {
                            loadMusic();
                        }, 600);
                    }
                },
                error: function (e) {
                    logger.error("playMusic error:" + e);
                    setTimeout(function () {
                        loadMusic();
                    }, 600);
                }
            });
        }

    }

});