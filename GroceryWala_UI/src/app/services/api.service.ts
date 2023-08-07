import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ProductModel } from '../Models/ProductModel';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  readonly ROOT_URL;
  constructor(private http: HttpClient) {
    this.ROOT_URL = "http://localhost:3000/api"; 
  }
 
// All main api calls

  getUserDetails(url: string, payload: any){
    return this.http.get(`${this.ROOT_URL}/${url}`, payload);
  }

  get(url: string){
    return this.http.get(`${this.ROOT_URL}/${url}`);
  }
  
  post(url: string, payload: any){
    return this.http.post(`${this.ROOT_URL}/${url}`, payload);
  }

  put(url: string, payload: ProductModel){
    return this.http.put(`${this.ROOT_URL}/${url}`, payload);
  }

  delete(url: string){
    return this.http.delete(`${this.ROOT_URL}/${url}`);
  }


}
