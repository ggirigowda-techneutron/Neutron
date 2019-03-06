export class Userclaimdto {
  userId: String;
  claimType: String;
  claimValue: String;

  // Constructor
  constructor(userId: String, claimType: String, claimValue: String) {
    this.userId = userId;
    this.claimType = claimType;
    this.claimValue = claimValue;
  }
}
