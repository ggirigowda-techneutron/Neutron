export class Claim {
  userId: String = undefined;
  claimType: String = undefined;
  claimValue: String = undefined;

  // Constructor
  constructor(userId: String, claimType: String, claimValue: String) {
    this.userId = userId;
    this.claimType = claimType;
    this.claimValue = claimValue;
  }
}
