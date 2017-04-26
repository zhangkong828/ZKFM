define(['config'], function (config) {
    return {
        debug: function (msg) {
            if (config.default.logger.debug)
                console.debug(msg);
        },
        info: function (msg) {
            console.info(msg);
        },
        warn: function (msg) {
            console.warn(msg);
        },
        error: function (msg) {
            console.error(msg);
        }
    }
});