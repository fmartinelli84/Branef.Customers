import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  public getById(id: number): Observable<any> {
    return this.http.get<any>(`${this.baseUrl}/customers/${id}`);
  }

  public getAll(): Observable<any[]> {
    return this.http.get<any[]>(`${this.baseUrl}/customers`);
  }

  public create(movement: any): Observable<any> {
    return this.http.post<any>(`${this.baseUrl}/customers`, movement);
  }

  public update(movement: any): Observable<any> {
    return this.http.put<any>(`${this.baseUrl}/customers/${movement.id}`, movement);
  }

  public delete(id: number): Observable<any> {
    return this.http.delete<any>(`${this.baseUrl}/customers/${id}`);
  }
}
