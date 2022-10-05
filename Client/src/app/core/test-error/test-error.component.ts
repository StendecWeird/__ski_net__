import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-test-error',
  templateUrl: './test-error.component.html',
  styleUrls: ['./test-error.component.scss']
})
export class TestErrorComponent implements OnInit {

  baseUrl = environment.apiUrl;
  validationErrors: Array<string>;

  constructor(private http:HttpClient) { }

  ngOnInit(): void {
  }

  get404NotFound() {
    this.getErrorFor("buggy/not-found");
  }

  get400BadRequest() {
    this.getErrorFor("buggy/bad-request");
  }

  get400BadRequestNotValidated() {
    this.getErrorFor("buggy/not-validated/fourth", (error) => {
      console.log(error);
      if (error.errors) {
        this.validationErrors = error.errors;
      }
    });
  }

  get500ServerError() {
    this.getErrorFor("buggy/server-error");
  }

  getErrorFor(path: string, errorHandler: (error: any) => void = console.log)
  {
    this.http.get(this.baseUrl + path).subscribe(response => {
      console.log(response);
    }, error => {
      errorHandler(error);
    });
  }

}
