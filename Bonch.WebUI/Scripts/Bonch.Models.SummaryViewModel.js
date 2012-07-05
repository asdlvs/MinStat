
$(document).ready(function () {

    function Summary(serverSummary) {
        var self = this;
        self.Id = serverSummary.Id;
        self.Title = serverSummary.Title;
        self.CreateDate = serverSummary.CreateDate;
        self.AuthorName = serverSummary.AuthorName;
        self.Published = serverSummary.Published;
        self.PersonsCount = serverSummary.PersonsCount;

    }

    function SummaryViewModel() {
        var self = this;
        self.summaries = ko.observableArray();
        self.activities = ko.observableArray();
        self.newSummaryName = ko.observable();
        self.selectedSummary = ko.observable();

        self.loadSummaries = function () {
            $.get('Summary/Summaries', self.summaries);
        }
        self.loadActivities = function () {
            $.get('Summary/Activities', self.activities);
        }

        self.showModalForActivities = function () {
            self.loadActivities();
            self.selectedSummary('');
            $('#myModal').modal('show');
        }
        self.saveSummary = function () {
            $('#savesumbtn').button('loading');
            $('#cancelsumbtn').button('loading');
            var summary;
            if (self.selectedSummary() == null || self.selectedSummary() == '') {
                summary = new Summary(
            {
                Title: self.newSummaryName(),
                CreateDate: '',
                AuthorName: '',
                Published: false,
                PersonsCount: 0

            });
            }
            else {
                summary = self.selectedSummary();
            }
            //setTimeout(function () {
            $.post('Summary/Save', { summary: summary, activities: ko.toJSON(self.activities) }, function (savedSummary) {
                self.summaries.remove(summary);
                self.summaries.unshift(savedSummary);
                self.newSummaryName('');
                $('#savesumbtn').button('complete');
                $('#cancelsumbtn').button('complete');
                $('#myModal').modal('hide');
            });
            //}, 3000);

        }

        self.selectSummary = function (summary) {
            self.selectedSummary(summary);
            $.get('Summary/Activities', { summary: summary }, self.activities);
            $('#myModal').modal('show');
        }

        self.loadSummaries();
    }

    ko.applyBindings(new SummaryViewModel());
});