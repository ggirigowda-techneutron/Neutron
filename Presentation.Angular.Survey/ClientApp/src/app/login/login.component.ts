import { Component, Input, EventEmitter, Output, OnInit } from "@angular/core";
import * as Survey from "survey-angular";
import * as widgets from "surveyjs-widgets";

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
  selector: 'app-login',
  template: '<div class="survey-container contentcontainer codecontainer"><div id="surveyElement"></div></div>'
})
export class LoginComponent implements OnInit {

  @Output()
  submitSurvey = new EventEmitter<any>();

  @Input()
  json: object = {
    "completedHtml": "You are succesfully logged in",
    "pages": [
      {
            "elements": [
              {
                "type": "text",
                "name": "userName",
                "title": "Username",
                "isRequired": true,
                "requiredErrorText": "Please enter username",
              }, {
                "type": "text",
                "name": "password",
                "inputType": "password",
                "startWithNewLine": true,
                "title": "Password",
                "isRequired": true,
                "requiredErrorText": "Please enter password",
              }
            ]
          }
    ],
    "completeText": "Login",
    "showQuestionNumbers": "off",
    "title": "Login"
  };

  ngOnInit() {
    const surveyModel = new Survey.Model(this.json);
    surveyModel.onAfterRenderQuestion.add((survey, options) => {
    });


    surveyModel.onComplete.add((sender, options) => {
      console.log(options);
      console.log(JSON.stringify(sender.data));
    });

    Survey.StylesManager.applyTheme("bootstrap");
    //Survey.defaultBootstrapCss.navigationButton = "btn btn-green";
    Survey.SurveyNG.render("surveyElement", { model: surveyModel });
  }

}
