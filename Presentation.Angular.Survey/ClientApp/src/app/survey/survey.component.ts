import { Component, Input, EventEmitter, Output, OnInit } from "@angular/core";
import * as Survey from "survey-angular";
import * as widgets from "surveyjs-widgets";

import "inputmask/dist/inputmask/phone-codes/phone.js";

widgets.icheck(Survey);
widgets.select2(Survey);
widgets.inputmask(Survey);
widgets.jquerybarrating(Survey);
widgets.jqueryuidatepicker(Survey);
widgets.nouislider(Survey);
widgets.select2tagbox(Survey);
widgets.signaturepad(Survey);
widgets.sortablejs(Survey);
widgets.ckeditor(Survey);
widgets.autocomplete(Survey);
widgets.bootstrapslider(Survey);
widgets.prettycheckbox(Survey);

@Component({
  // tslint:disable-next-line:component-selector
  selector: "survey",
  template: '<div class="survey-container contentcontainer codecontainer"><div id="surveyElement"></div><div id="surveyResult"></div></div>'
})
export class SurveyComponent implements OnInit {
  
  @Output()
  submitSurvey = new EventEmitter<any>();

  @Input()
  json: object = {
    "completedHtml": "Thank you for this.......",
    "pages": [
      {
        "elements": [
          {
            "type": "panel",
            "name": "patienName",
            "elements": [
              {
                "type": "text",
                "name": "patientLastName",
                "title": "(Last)",
                "isRequired": true
              }, {
                "type": "text",
                "name": "patienFirstName",
                "startWithNewLine": false,
                "title": "(First)",
                "isRequired": true
              }, {
                "type": "text",
                "name": "patientMiddleName",
                "startWithNewLine": false,
                "title": "(M.I)",
                "isSerializable": true
              }
            ],
            "questionTitleLocation": "bottom",
            "title": "Patient Name"
          }, {
            "type": "panel",
            "name": "panel2",
            "elements": [
              {
                "type": "text",
                "name": "socialsecurity",
                "title": "Social Security #:",
                "isRequired": true,
                "inputFormat": "999-99-9999"
              }, {
                "type": "text",
                "inputType": "date",
                "isRequired": true,
                "name": "birthDate",
                "startWithNewLine": false,
                "title": "Date of birth:"
              }, {
                "type": "radiogroup",
                "choices": [
                  {
                    "value": "male",
                    "text": "Male"
                  }, {
                    "value": "female",
                    "text": "Female"
                  }
                ],
                "colCount": 0,
                "isRequired": true,
                "name": "sex",
                "startWithNewLine": false,
                "title": "Sex:"
              }, {
                "type": "panel",
                "name": "education",
                "elements": [
                  {
                    "type": "dropdown",
                    "name": "schoolYearsCompleted",
                    "title": "How many yeas of school have you completed?",
                    "isRequired": true,
                    "choices": [
                      "0",
                      "1",
                      "2",
                      "3",
                      "4",
                      "5",
                      "6",
                      "7",
                      "8",
                      "9",
                      "10",
                      "11",
                      "12"
                    ]
                  }
                ],
                "title": "Education"
              }
            ],
            "questionTitleLocation": "left",
            "title": "Social Security & Birth Date"
          }, {
            "type": "panel",
            "name": "panel1",
            "elements": [
              {
                "type": "radiogroup",
                "choices": [
                  {
                    "value": "patient",
                    "text": "Patient"
                  }, {
                    "value": "spouse",
                    "text": "Spouse"
                  }
                ],
                "colCount": 0,
                "hasOther": true,
                "isRequired": true,
                "name": "completedBy",
                "otherText": "Other (specify)",
                "title": "Who completed this form:"
              }, {
                "type": "text",
                "name": "completedByOtherName",
                "visibleIf": "{completedBy} != \"patient\"",
                "startWithNewLine": false,
                "title": "Name (if other than patient):",
                "isRequired": true
              }
            ],
            "title": "Completed By"
          }, {
            type: "matrix",
            name: "Quality",
            title: "Please indicate if you agree or disagree with the following statements",
            columns: [
              {
                value: 1,
                text: "Strongly Disagree"
              }, {
                value: 2,
                text: "Disagree"
              }, {
                value: 3,
                text: "Neutral"
              }, {
                value: 4,
                text: "Agree"
              }, {
                value: 5,
                text: "Strongly Agree"
              }
            ],
            rows: [
              {
                value: "affordable",
                text: "Product is affordable"
              }, {
                value: "does what it claims",
                text: "Product does what it claims"
              }, {
                value: "better then others",
                text: "Product is better than other products on the market"
              }, {
                value: "easy to use",
                text: "Product is easy to use"
              }
            ]
          }
        ],
        "name": "personaldetails",
        "title": "Personal Details"
      }
      ],
    "startSurveyText": "Begin",
    "pagePrevText": "Previous",
    "pageNextText": "Next",
    "completeText": "Submit",
    "showProgressBar": "top",
    "showQuestionNumbers": "on",
    "title": "User Information"
  };

  ngOnInit() {
    const surveyModel = new Survey.Model(this.json);
    surveyModel.onAfterRenderQuestion.add((survey, options) => {
    });

    //surveyModel.onComplete
    //  .add(result => {
    //      this.submitSurvey.emit(result.data);
    //      console.log(JSON.stringify(result.data));
    //    }
    //  );

    surveyModel.onComplete
      .add(result => {
        document
          .querySelector('#surveyResult')
          .innerHTML = "result: " + JSON.stringify(result.data);
      });


    surveyModel.onComplete.add((sender, options) => {
      console.log(options);
      console.log(JSON.stringify(sender.data));
    });

    surveyModel.data = {
      patientLastName: "Doe",
      patienFirstName: "John",
      sex: "female",
      schoolYearsCompleted: 1,
      socialsecurity: "123-45-6789",
      birthDate: new Date("2015-03-25"),
      completedBy: "patient"
    };

    Survey.StylesManager.applyTheme("bootstrap");
    //Survey.defaultBootstrapCss.navigationButton = "btn btn-green";
    Survey.SurveyNG.render("surveyElement", { model: surveyModel });
  }
}
