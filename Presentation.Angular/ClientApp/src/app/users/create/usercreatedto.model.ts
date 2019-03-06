import { Userclaimdto } from "./userclaimdto.model";
import { Userprofiledto } from "./userprofiledto.model";

export class Usercreatedto {
  ci: Number;
  id: String;
  userName: String;
  email: String;
  emailConfirmed: boolean;
  password: String;
  phoneNumberConfirmed: boolean;
  mobileNumberConfirmed: boolean;
  nationalId: Number;
  accessFailedCount: Number;
  createdOn: Date;
  changedOn: Date;
  profile: Userprofiledto;
  claims: Array<Userclaimdto>;

  constructor(ci: Number,
    id: String,
    userName: String,
    email: String,
    password: String,
    nationalId: Number,
    profile: Userprofiledto,
    claims: Array<Userclaimdto>) {
    const date = new Date(2019, 3, 1);
    this.ci = ci;
    this.id = id;
    this.userName = userName;
    this.email = email;
    this.emailConfirmed = true;
    this.password = password;
    this.phoneNumberConfirmed = true;
    this.mobileNumberConfirmed = true;
    this.nationalId = nationalId;
    this.accessFailedCount = 0;
    this.createdOn = date;
    this.changedOn = date;
    this.profile = profile;
    this.claims = claims;
  }
}
