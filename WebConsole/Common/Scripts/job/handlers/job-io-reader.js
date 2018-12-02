﻿/* WebConsole (https://github.com/hirov-anton/web-console)
See LICENSE file in the solution root for full license information
Copyright (c) 2018 Anton Hirov */

function startJob(job, args, callback) {
    var stream = $.connection.streamHub;
    stream.client.read = function (id, value) {
        var job = loadJob(id);
        if (job.isActive) {
            job.status = "working...";

            var container = getWindowContainer(job.id);
            container.find(wcWindowStatusClass).text(job.status);
            $.CreateParagraph()
             .append(value)
             .appendTo(container.find(wcWindowContentOutputClass));
        }
        job.lines.push(value);
        saveJob(job);
    };
    stream.client.stop = function(id) {stopJob(id);};

    $.connection.hub.start().done(function () {
        stream.server.startJob(job.location,
                               args).done(function (id) {
            job.id = id;
            job.lines = [];
            job.status = "initializing...";
            job.isActive = true;

            saveJob(job);
            callback();
        });
    });
}

function stopJob(id) {
    var stream = $.connection.streamHub;
    stream.server.stopJob(id).done(function () {
        removeJob(id);
        createDefaultSign(id);
    });
}