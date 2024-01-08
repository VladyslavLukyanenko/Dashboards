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
import { ProductData } from './product-data.model';
import { ApiError } from './api-error.model';


export interface ProductDataIListApiContract { 
    error?: ApiError;
    readonly payload?: Array<ProductData> | null;
}
