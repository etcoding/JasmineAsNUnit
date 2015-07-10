(function () {

    function ReportCollector() {
    }

    ReportCollector.prototype.jasmineStarted = function (data) {
        ReportCollector.Status = "Started";
    };
    ReportCollector.prototype.jasmineDone = function () {
        ReportCollector.Status = "Finished";
    };
    ReportCollector.prototype.suiteStarted = function (result) {
    };
    ReportCollector.prototype.suiteDone = function (result) {
        ReportCollector.Results.push({ type: "suite", result: result });
    };
    ReportCollector.prototype.specstarted = function (specResult) {
        specResult.startTime = new Date().getTime();
    };
    ReportCollector.prototype.specDone = function (specResult) {
        ReportCollector.Results.push({ type: "spec", result: specResult });
        ReportCollector.SpecResults.push(specResult);
    };

    ReportCollector.Results = [];
    ReportCollector.SpecResults = [];
    ReportCollector.Status = "";

    window.ReportCollector = ReportCollector;
})();





(function () {

    window.jasmine = jasmineRequire.core(jasmineRequire);

    var env = jasmine.getEnv();
    var jasmineInterface = jasmineRequire.interface(jasmine, env);
    extend(window, jasmineInterface);

    env.addReporter(new window.ReportCollector());

    var currentWindowOnload = window.onload;

    window.onload = function () {
        if (currentWindowOnload) {
            currentWindowOnload();
        }
    };


    function extend(destination, source) {
        for (var property in source) destination[property] = source[property];
        return destination;
    }
}());

