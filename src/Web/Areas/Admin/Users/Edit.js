var Admin;
(function (Admin) {
  var Users;
  (function (Users) {
    class editVueModel {
      constructor(hub, model) {
        this.hub = hub;
        this.model = model;
        if (this.hub) {
          this.hub.on("NewMessage", async (idUser, idMessage) => {
            // do stuff with parameters
          });
        }
      }
    }
    Users.editVueModel = editVueModel;
  })((Users = Admin.Users || (Admin.Users = {})));
})(Admin || (Admin = {}));
//# sourceMappingURL=Edit.js.map
