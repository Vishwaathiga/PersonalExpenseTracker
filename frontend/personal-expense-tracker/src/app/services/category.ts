
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Category {
  id: number;
  name: string;
  isDefault: boolean;
}

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  private apiUrl = 'https://localhost:7082/api/category';

  constructor(private http: HttpClient) {}

  private getHeaders(): HttpHeaders {
    const token = localStorage.getItem('token');

    return new HttpHeaders({
      Authorization: `Bearer ${token}`
    });
  }

  getCategories(): Observable<Category[]> {
    return this.http.get<Category[]>(
      this.apiUrl,
      { headers: this.getHeaders() }
    );
  }

  addCategory(name: string): Observable<Category> {
    return this.http.post<Category>(
      this.apiUrl,
      { name },
      { headers: this.getHeaders() }
    );
  }

  updateCategory(id: number, name: string): Observable<Category> {
    return this.http.put<Category>(
      `${this.apiUrl}/${id}`,
      { name },
      { headers: this.getHeaders() }
    );
  }

  deleteCategory(id: number): Observable<any> {
    return this.http.delete(
      `${this.apiUrl}/${id}`,
      { headers: this.getHeaders() }
    );
  }
}
