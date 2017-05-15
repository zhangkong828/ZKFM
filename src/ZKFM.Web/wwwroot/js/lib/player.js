define(['jquery', 'logger'], function ($, logger) {
    audio = document.getElementById("music");
    function Lyc() {
        console.log("lyc")
    }
    return {
        play: function (data) {
            audio.play();
        },
        pause: function () {
            audio.pause();
        },
        getloop: function () {
            return audio.loop;
        },
        setloop: function (isloop) {
            audio.loop = isloop;
        },
        getprogress: function () {
            if (audio.currentTime >= audio.duration)
                return 100;
            return audio.currentTime / audio.duration * 100;
        }
    }
});