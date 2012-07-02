$(document).ready(function () {

    function PersonViewModel() {
        var self = this;
        self.undeliveredSummaries = ko.observable();
        self.selectedSummary = ko.observable();
        self.summaryPersons = ko.observableArray();
        self.selectedActivity = ko.observable();
        self.summaryActivities = ko.observableArray();

        self.selectSummary = function (summary) {
            self.selectedSummary(summary);
            self.selectActivity(null);
            $.get('Person/Persons', { summary: summary }, self.summaryPersons);
            $.get('Person/Activities', { summary: summary }, self.summaryActivities);
        }

        self.selectActivity = function (activity) {
            self.selectedActivity(activity);
            $.get('Person/Persons', { summary: self.selectedSummary(), activity: activity }, self.summaryPersons);
        }

        self.loadUndeliveredSummaries = function () {
            $.get('Person/UndeliveredSummaries', self.undeliveredSummaries);
        }



        self.loadUndeliveredSummaries();
    }


    ko.applyBindings(new PersonViewModel());
});

