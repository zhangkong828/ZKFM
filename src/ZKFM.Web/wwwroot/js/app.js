requirejs.config({
    baseUrl: 'js/lib',
    paths: {
        jquery: '../jquery.min',
        lib: '../lib'
    }
});

requirejs(['jquery', 'lib/controller', 'lib/config', 'lib/logger', 'lib/player'], function ($, controller, config, logger, player) {
    //var music = config.default.music;
    //player.Play(music[0]);

});