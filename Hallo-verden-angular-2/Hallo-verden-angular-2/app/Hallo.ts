import {Component} from "@angular/core";

@Component({
    selector: "min-app",
    template: "<h1>{{melding}}</h1>"
})
export class Hallo {

    public melding: string;

    constructor() {
        this.melding = "Hei og hå hvor det går!";
    }
}
