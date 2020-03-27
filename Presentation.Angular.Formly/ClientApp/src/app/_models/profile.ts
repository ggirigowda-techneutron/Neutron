export class Profile {
  ci: Number = undefined;
  userId: String = undefined;
  firstName: String = undefined;
  lastName: String = undefined;
  userTypeId: String = undefined;
  dob: Date = undefined;
  genderId: String = undefined;
  countryId: String = undefined;

  constructor(ci: Number, userId: String, firstName: String, lastName: String, dob: Date, countryId: String) {
    this.ci = ci;
    this.userId = userId;
    this.firstName = firstName;
    this.lastName = lastName;
    this.userTypeId = "5ebf5cca-df92-49c6-ae5f-f3c9670bf9d3";
    this.dob = dob;
    this.genderId = "8a29a4ab-62a7-4a06-b2fa-46a40f449a84";
    this.countryId = countryId;
  }
}
