import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class FeatureService {

  serverApi = "https://localhost:44371"
  constructor(private http: HttpClient) { }

  getFeatures() {
    return this.http.get(`${this.serverApi}/api/features`);
  }
}
