
$(document).ready(function () {

  function UserViewModel() {
    var self = this;
    self.id = ko.observable();
    self.login = ko.observable();

    self.firstName = ko.observable();
    self.lastName = ko.observable();
    self.phone = ko.observable();
    self.save = ko.observable(true);
    self.historyPoints = ko.observable();

    self.identity = ko.computed(function () {

      var result = self.firstName() != null && self.firstName() != ""
                    && self.lastName() != null && self.lastName() != ""
                    && self.phone() != null && self.phone() != "";
      self.save(result);
      return result;

    });


    self.constructor = function (user) {
      self.id(user.Id);
      self.login(user.Login);
      self.firstName(user.FirstName);
      self.lastName(user.LastName);
      self.phone(user.Phone);
      self.getHistoryPoints();
    };

    self.getCurrentUser = function () {
      $.get('Home/User', self.constructor);
    }

    self.saveCurrentUser = function () {
      if (self.identity()) {
        $.post('Home/UpdateUser', { firstName: self.firstName(), lastName: self.lastName(), phone: self.phone() },
          function () { self.save(true); }
        )
      } else {

        $('.errorsummary').html('Все поля обязательны для заполнения.');
      }
    };

    self.getHistoryPoints = function() {
      if (self.identity()) {
        $.get('Home/HistoryPoints', self.historyPoints);
      }
    };

    self.getCurrentUser();


  }

  ko.applyBindings(new UserViewModel());
});