
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

        self.loadSummaries = function () {
            $.get('Summary/Summaries', self.summaries);
        }
        self.loadActivities = function () {
            $.get('Summary/Activities', self.activities);
        }

        self.showModalForActivities = function () {
            $('#myModal').modal('show');
        }
        self.addNewSummary = function () {
            var newSummary = new Summary(
            {
                Title: self.newSummaryName(),
                CreateDate: '',
                AuthorName: '',
                Published: false,
                PersonsCount: 0

            })
            self.summaries.unshift(newSummary);
            //setTimeout(function () {)
            $.post('Summary/Add', { summary: newSummary, activities: ko.toJSON(self.activities) }, function (savedSummary) {
                self.summaries.remove(newSummary);
                self.summaries.unshift(savedSummary);
                self.newSummaryName('');
            });
            //}, 3000);

        }



        self.loadSummaries();
        self.loadActivities();
    }

    ko.applyBindings(new SummaryViewModel());
});