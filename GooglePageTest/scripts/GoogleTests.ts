/// <reference path="typings/jasmine/jasmine.d.ts" />
/// <reference path="typings/jquery/jquery.d.ts" />
describe("Results page", function () {
    it("should contain multiple entries", function () {
        var results = $("li.g");
        expect(results.length).toBeGreaterThan(1);
    });

    it("should contain a link to Wikipedia", function () {
        var linksText = $("a").text();
        expect(linksText).toContain("Test - Wikipedia, the free encyclopedia");
    });
});

describe("Navigation",() => {
    it("should contain links to 10 pages",() => {
        var pages = $("table#nav td");

        var text = $("table#nav td a").text();
        for (var i = 1; i < 11; i++) {
            expect(text).toContain(i.toString());
        }
    });
});