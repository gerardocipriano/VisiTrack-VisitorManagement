module Example.Users {
    export class editVueModel {
        constructor(public hub: any, public model: Example.Users.Server.editViewModel) {
            if (this.hub) {
                this.hub.on("NewMessage", async (idUser: any, idMessage: any) => {
                    // do stuff with parameters
                });
            }
        }
    }
}