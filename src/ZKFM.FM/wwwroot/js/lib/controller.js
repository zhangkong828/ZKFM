define(['jquery', 'lib/logger'], function ($, logger) {

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
            addMusic({ id: id, name: name });
        }
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
       
    };

    return {
        AddMusic: addMusic,
        AddMusic2: addMusic2,
        DelMusic: delMusic
    };

});