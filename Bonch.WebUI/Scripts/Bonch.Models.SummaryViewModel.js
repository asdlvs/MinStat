
$(document).ready(function () {


    function SummaryViewModel() {
        var self = this;
        self.summaries = ko.observableArray();
        self.activities = ko.observableArray();
        self.newSummaryName = ko.observable();
        self.selectedSummary = ko.observable();
        self.published = ko.observable();

        self.loadActivities = function () {
            $.get('Summary/Activities', self.activities);
        }

        self.showModalForActivities = function () {
            if (self.newSummaryName() == '' || self.newSummaryName() == undefined) {
                alert('Укажите название нового блока!');
            }
            else {
                self.loadActivities();
                self.selectedSummary('');
                self.published(false);
                $('#myModal').modal('show');
            }
        }
        self.saveSummary = function () {
            $('#savesumbtn').button('loading');
            $('#cancelsumbtn').button('loading');

            $.post('Summary/Save', { id: self.selectedSummary(), title: self.newSummaryName(), activities: ko.toJSON(self.activities) },
            function () { location.replace(document.location) })
            .error(function () {
                alert('Невозможно удалить активность, так как к ней уже есть привязанные люди. Изменения не были сохранены.');
                self.selectSummary(self.selectedSummary());
                $('#savesumbtn').button('complete');
                $('#cancelsumbtn').button('complete');
            });
        }

        self.selectSummary = function (summary, pub) {
            self.published(pub);
            self.selectedSummary(summary);
            $.get('Summary/Activities?Id=' + summary, null, self.activities);
            $('#myModal').modal('show');
        }

        self.copySummary = function (summary) {
            if (self.newSummaryName() == '' || self.newSummaryName() == undefined) {
                alert('Укажите название нового блока!');
            }
            else {
                $.post('Summary/Copy', { oldId: summary, newTitle: self.newSummaryName() }, function () { location.replace(document.location) });
            }
        }

        self.publish = function (summary) {
            if (confirm('Вы уверены, что хотите опубликовать данный блок? После опубликования будет невозможно вносить туда какие-либо изменения!')) {
                $.post('Summary/Publish', { id: summary }, function () { location.replace(document.location) });
            }
        }

    }

    ko.applyBindings(new SummaryViewModel());
});