/**
 * Dashboards API
 * No description provided (generated by Openapi Generator https://github.com/openapitools/openapi-generator)
 *
 * The version of the OpenAPI document: v1
 * 
 *
 * NOTE: This class is auto generated by OpenAPI Generator (https://openapi-generator.tech).
 * https://openapi-generator.tech
 * Do not edit the class manually.
 */
/* tslint:disable:no-unused-variable member-ordering */

import { Inject, Injectable, Optional }                      from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams,
         HttpResponse, HttpEvent, HttpParameterCodec }       from '@angular/common/http';
import { CustomHttpParameterCodec }                          from '../encoder';
import { Observable }                                        from 'rxjs';

import { ChangeSetDataIPagedListApiContract } from '../model/models';
import { ChangeType } from '../model/models';
import { ChangesetEntryPayloadDataApiContract } from '../model/models';
import { ProblemDetails } from '../model/models';

import { BASE_PATH, COLLECTION_FORMATS }                     from '../variables';
import { Configuration }                                     from '../configuration';



@Injectable({
  providedIn: 'root'
})
export class AuditService {

    protected basePath = 'http://localhost';
    public defaultHeaders = new HttpHeaders();
    public configuration = new Configuration();
    public encoder: HttpParameterCodec;

    constructor(protected httpClient: HttpClient, @Optional()@Inject(BASE_PATH) basePath: string, @Optional() configuration: Configuration) {
        if (configuration) {
            this.configuration = configuration;
        }
        if (typeof this.configuration.basePath !== 'string') {
            if (typeof basePath !== 'string') {
                basePath = this.basePath;
            }
            this.configuration.basePath = basePath;
        }
        this.encoder = this.configuration.encoder || new CustomHttpParameterCodec();
    }


    private addToHttpParams(httpParams: HttpParams, value: any, key?: string): HttpParams {
        if (typeof value === "object" && value instanceof Date === false) {
            httpParams = this.addToHttpParamsRecursive(httpParams, value);
        } else {
            httpParams = this.addToHttpParamsRecursive(httpParams, value, key);
        }
        return httpParams;
    }

    private addToHttpParamsRecursive(httpParams: HttpParams, value?: any, key?: string): HttpParams {
        if (value == null) {
            return httpParams;
        }

        if (typeof value === "object") {
            if (Array.isArray(value)) {
                (value as any[]).forEach( elem => httpParams = this.addToHttpParamsRecursive(httpParams, elem, key));
            } else if (value instanceof Date) {
                if (key != null) {
                    httpParams = httpParams.append(key,
                        (value as Date).toISOString().substr(0, 10));
                } else {
                   throw Error("key may not be null if value is Date");
                }
            } else {
                Object.keys(value).forEach( k => httpParams = this.addToHttpParamsRecursive(
                    httpParams, value[k], key != null ? `${key}.${k}` : k));
            }
        } else if (key != null) {
            httpParams = httpParams.append(key, value);
        } else {
            throw Error("key may not be null if value is not object or array");
        }
        return httpParams;
    }

    /**
     * @param entryId 
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public auditGetChangeSetEntryPayloadPage(entryId: string, observe?: 'body', reportProgress?: boolean, options?: {httpHeaderAccept?: 'text/plain' | 'application/json' | 'text/json'}): Observable<ChangesetEntryPayloadDataApiContract>;
    public auditGetChangeSetEntryPayloadPage(entryId: string, observe?: 'response', reportProgress?: boolean, options?: {httpHeaderAccept?: 'text/plain' | 'application/json' | 'text/json'}): Observable<HttpResponse<ChangesetEntryPayloadDataApiContract>>;
    public auditGetChangeSetEntryPayloadPage(entryId: string, observe?: 'events', reportProgress?: boolean, options?: {httpHeaderAccept?: 'text/plain' | 'application/json' | 'text/json'}): Observable<HttpEvent<ChangesetEntryPayloadDataApiContract>>;
    public auditGetChangeSetEntryPayloadPage(entryId: string, observe: any = 'body', reportProgress: boolean = false, options?: {httpHeaderAccept?: 'text/plain' | 'application/json' | 'text/json'}): Observable<any> {
        if (entryId === null || entryId === undefined) {
            throw new Error('Required parameter entryId was null or undefined when calling auditGetChangeSetEntryPayloadPage.');
        }

        let headers = this.defaultHeaders;

        let credential: string | undefined;
        // authentication (Bearer) required
        credential = this.configuration.lookupCredential('Bearer');
        if (credential) {
            headers = headers.set('Authorization', credential);
        }

        let httpHeaderAcceptSelected: string | undefined = options && options.httpHeaderAccept;
        if (httpHeaderAcceptSelected === undefined) {
            // to determine the Accept header
            const httpHeaderAccepts: string[] = [
                'text/plain',
                'application/json',
                'text/json'
            ];
            httpHeaderAcceptSelected = this.configuration.selectHeaderAccept(httpHeaderAccepts);
        }
        if (httpHeaderAcceptSelected !== undefined) {
            headers = headers.set('Accept', httpHeaderAcceptSelected);
        }


        let responseType: 'text' | 'json' = 'json';
        if(httpHeaderAcceptSelected && httpHeaderAcceptSelected.startsWith('text')) {
            responseType = 'text';
        }

        return this.httpClient.get<ChangesetEntryPayloadDataApiContract>(`${this.configuration.basePath}/v1/audit/${encodeURIComponent(String(entryId))}`,
            {
                responseType: <any>responseType,
                withCredentials: this.configuration.withCredentials,
                headers: headers,
                observe: observe,
                reportProgress: reportProgress
            }
        );
    }

    /**
     * @param facilityId 
     * @param userId 
     * @param changeType 
     * @param from 
     * @param to 
     * @param pageIndex 
     * @param orderBy 
     * @param limit 
     * @param offset 
     * @param isOrdered 
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public auditGetChangesPage(facilityId?: number, userId?: number, changeType?: ChangeType, from?: string, to?: string, pageIndex?: number, orderBy?: string, limit?: number, offset?: number, isOrdered?: boolean, observe?: 'body', reportProgress?: boolean, options?: {httpHeaderAccept?: 'text/plain' | 'application/json' | 'text/json'}): Observable<ChangeSetDataIPagedListApiContract>;
    public auditGetChangesPage(facilityId?: number, userId?: number, changeType?: ChangeType, from?: string, to?: string, pageIndex?: number, orderBy?: string, limit?: number, offset?: number, isOrdered?: boolean, observe?: 'response', reportProgress?: boolean, options?: {httpHeaderAccept?: 'text/plain' | 'application/json' | 'text/json'}): Observable<HttpResponse<ChangeSetDataIPagedListApiContract>>;
    public auditGetChangesPage(facilityId?: number, userId?: number, changeType?: ChangeType, from?: string, to?: string, pageIndex?: number, orderBy?: string, limit?: number, offset?: number, isOrdered?: boolean, observe?: 'events', reportProgress?: boolean, options?: {httpHeaderAccept?: 'text/plain' | 'application/json' | 'text/json'}): Observable<HttpEvent<ChangeSetDataIPagedListApiContract>>;
    public auditGetChangesPage(facilityId?: number, userId?: number, changeType?: ChangeType, from?: string, to?: string, pageIndex?: number, orderBy?: string, limit?: number, offset?: number, isOrdered?: boolean, observe: any = 'body', reportProgress: boolean = false, options?: {httpHeaderAccept?: 'text/plain' | 'application/json' | 'text/json'}): Observable<any> {

        let queryParameters = new HttpParams({encoder: this.encoder});
        if (facilityId !== undefined && facilityId !== null) {
          queryParameters = this.addToHttpParams(queryParameters,
            <any>facilityId, 'facilityId');
        }
        if (userId !== undefined && userId !== null) {
          queryParameters = this.addToHttpParams(queryParameters,
            <any>userId, 'userId');
        }
        if (changeType !== undefined && changeType !== null) {
          queryParameters = this.addToHttpParams(queryParameters,
            <any>changeType, 'changeType');
        }
        if (from !== undefined && from !== null) {
          queryParameters = this.addToHttpParams(queryParameters,
            <any>from, 'from');
        }
        if (to !== undefined && to !== null) {
          queryParameters = this.addToHttpParams(queryParameters,
            <any>to, 'to');
        }
        if (pageIndex !== undefined && pageIndex !== null) {
          queryParameters = this.addToHttpParams(queryParameters,
            <any>pageIndex, 'pageIndex');
        }
        if (orderBy !== undefined && orderBy !== null) {
          queryParameters = this.addToHttpParams(queryParameters,
            <any>orderBy, 'orderBy');
        }
        if (limit !== undefined && limit !== null) {
          queryParameters = this.addToHttpParams(queryParameters,
            <any>limit, 'limit');
        }
        if (offset !== undefined && offset !== null) {
          queryParameters = this.addToHttpParams(queryParameters,
            <any>offset, 'offset');
        }
        if (isOrdered !== undefined && isOrdered !== null) {
          queryParameters = this.addToHttpParams(queryParameters,
            <any>isOrdered, 'isOrdered');
        }

        let headers = this.defaultHeaders;

        let credential: string | undefined;
        // authentication (Bearer) required
        credential = this.configuration.lookupCredential('Bearer');
        if (credential) {
            headers = headers.set('Authorization', credential);
        }

        let httpHeaderAcceptSelected: string | undefined = options && options.httpHeaderAccept;
        if (httpHeaderAcceptSelected === undefined) {
            // to determine the Accept header
            const httpHeaderAccepts: string[] = [
                'text/plain',
                'application/json',
                'text/json'
            ];
            httpHeaderAcceptSelected = this.configuration.selectHeaderAccept(httpHeaderAccepts);
        }
        if (httpHeaderAcceptSelected !== undefined) {
            headers = headers.set('Accept', httpHeaderAcceptSelected);
        }


        let responseType: 'text' | 'json' = 'json';
        if(httpHeaderAcceptSelected && httpHeaderAcceptSelected.startsWith('text')) {
            responseType = 'text';
        }

        return this.httpClient.get<ChangeSetDataIPagedListApiContract>(`${this.configuration.basePath}/v1/audit`,
            {
                params: queryParameters,
                responseType: <any>responseType,
                withCredentials: this.configuration.withCredentials,
                headers: headers,
                observe: observe,
                reportProgress: reportProgress
            }
        );
    }

}
