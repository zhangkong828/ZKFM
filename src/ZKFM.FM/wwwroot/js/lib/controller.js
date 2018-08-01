define(['jquery', 'logger', 'player'], function ($, logger, player) {

    function getIndex(id) {
        for (var j = 0; j < musicList.length; j++) {
            if (musicList[j].id === id) {
                return j;
            }
        }
        return 0;
    };

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
            if (id == $('.musicid').text()) {
                this.LoadMusic();
            }
        },
        playMusic: function (id) {
            logger.debug("play:" + id);
            currentIndex = getIndex(id);
            $("#lrc-lines").css(" margin-top", "0px").html('');
            //clearInterval(timeout);
            //clearInterval(lrctimeout);
            $('#wrap .progress .current').css({ 'width': '0%' });
            $.ajax({
                type: "get",
                url: "/API/NetEaseMusic/V1/Get/" + id,
                cache: false,
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
                        player.Play(data.src);
                    } else {
                        logger.info(response.msg);
                    }
                },
                error: function (e) {
                    logger.error("playMusic error:" + e);
                }
            });
        },
        LoadMusic: function () {
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
        }

    }

});