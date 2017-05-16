define(['jquery', 'logger'], function ($, logger) {
    audio = document.getElementById("music");
    function Lyc() {
        console.log("lyc")
    }
    return {
        Play: function (data) {
            audio.play();
        },
        Pause: function () {
            audio.pause();
        },
        GetLoop: function () {
            return audio.loop;
        },
        SetLoop: function (isloop) {
            audio.loop = isloop;
        },
        GetProgress: function () {
            if (audio.currentTime >= audio.duration)
                return 100;
            return audio.currentTime / audio.duration * 100;
        }
    }
});