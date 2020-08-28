import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class VehicleService {
  serverApi="https://localhost:44371";
  constructor(private http:HttpClient) { }

  createVehicle(vehicle){
    return this.http.post(`${this.serverApi}/api/vehicles`, vehicle);
    //.map(res => res.json());
  }
}
