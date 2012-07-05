$(document).ready(function () {

    function PersonViewModel() {
        var self = this;
        self.undeliveredSummaries = ko.observable();
        self.selectedSummary = ko.observable();
        self.summaryPersons = ko.observableArray();
        self.selectedActivity = ko.observable();
        self.summaryActivities = ko.observableArray();
        self.editingPerson = ko.observable();
        self.fullName = ko.computed(function () {
            if (self.editingPerson() != null && self.editingPerson().FirstName != null) {
                return self.editingPerson().FirstName + ' ' + self.editingPerson().LastName;
            }
            return null;
        });

        self.selectSummary = function (summary) {
            self.selectedSummary(summary);

            //self.selectActivity(null);
            //$.get('Person/Persons', { summary: summary }, self.summaryPersons);
            $.get('Person/Activities', { summary: summary }, function (data) {
                self.summaryActivities(data);
                $('#sum').html(summary.Title);
            });
        }

        self.selectActivity = function (activity) {
            self.selectedActivity(activity);
            $.get('Person/List', { summary: self.selectedSummary(), activity: activity }, function (data) {
                self.summaryPersons(data);
                $('#act').html(activity.Title);
            });
        }

        self.loadUndeliveredSummaries = function () {
            $.get('Person/UndeliveredSummaries', self.undeliveredSummaries);
        }

        self.showPersonPopup = function () {
            $('#myModal').modal('show');
        }

        self.selectPersonToEdit = function (person) {
            self.showPersonPopup();
            self.editingPerson(person);
        }

        self.createNewPersonPopup = function () {
            $.post('Person/Set', self.editingPerson);
            $('#myModal').modal('show');
        }


        self.savePerson = function () {
            self.summaryPersons.remove(self.editingPerson());
            $.post('Person/Set', { person: self.editingPerson(), activity: self.selectedActivity(), summary: self.selectedSummary() }, function (data) {
                self.editingPerson(data);
                self.summaryPersons.unshift(self.editingPerson());
                $('#savepersonbtn').button('complete');
            });
        }

        self.savePersonAndClose = function () {
            self.savePerson();
            $('#myModal').modal('hide');
        }

        self.deletePerson = function (person) {
            $.post('Person/Delete', { person: person }, function () { self.summaryPersons.remove(person); });
        }

        self.loadUndeliveredSummaries();
    }


    ko.applyBindings(new PersonViewModel());
});

