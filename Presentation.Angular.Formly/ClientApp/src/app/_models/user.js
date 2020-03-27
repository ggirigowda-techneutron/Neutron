"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var User = /** @class */ (function () {
    function User(ci, userName, email) {
        if (userName === void 0) { userName = ""; }
        if (email === void 0) { email = ""; }
        this.ci = undefined;
        this.id = undefined;
        this.userName = undefined;
        this.email = undefined;
        this.emailConfirmed = undefined;
        this.password = undefined;
        this.phoneNumberConfirmed = undefined;
        this.mobileNumberConfirmed = undefined;
        this.nationalId = undefined;
        this.accessFailedCount = undefined;
        this.createdOn = undefined;
        this.changedOn = undefined;
        this.profile = undefined;
        this.claims = undefined;
        this.ci = ci;
        this.userName = userName;
        this.email = email;
    }
    return User;
}());
exports.User = User;
//# sourceMappingURL=user.js.map