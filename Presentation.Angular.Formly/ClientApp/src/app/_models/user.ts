import { Claim } from "./claim";
import { Profile } from "./profile";
export class User {
  ci: Number = undefined;
  id: String = undefined;
  userName: String = undefined;
  email: String = undefined;
  emailConfirmed: boolean = undefined;
  password: String = undefined;
  phoneNumberConfirmed: boolean = undefined;
  mobileNumberConfirmed: boolean = undefined;
  nationalId: Number = undefined;
  accessFailedCount: Number = undefined;
  createdOn: Date = undefined;
  changedOn: Date = undefined;
  profile: Profile = undefined;
  claims: Array<Claim> = undefined;
  constructor(ci: Number, userName: String = "", email: String = "") {
    this.ci = ci;
    this.userName = userName;
    this.email = email;
  }
}
