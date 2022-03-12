/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { LoginRequest } from '../models/LoginRequest';
import type { Product } from '../models/Product';

import type { CancelablePromise } from '../core/CancelablePromise';
import { OpenAPI } from '../core/OpenAPI';
import { request as __request } from '../core/request';

export class MinApiDemoVersion1000CultureNeutralPublicKeyTokenNullService {

    /**
     * @returns any Success
     * @throws ApiError
     */
    public static postLogin({
        requestBody,
    }: {
        requestBody: LoginRequest,
    }): CancelablePromise<any> {
        return __request(OpenAPI, {
            method: 'POST',
            url: '/login',
            body: requestBody,
            mediaType: 'application/json',
        });
    }

    /**
     * @returns Product Success
     * @throws ApiError
     */
    public static getProducts(): CancelablePromise<Array<Product>> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/products',
        });
    }

    /**
     * @returns any Success
     * @throws ApiError
     */
    public static postProducts({
        requestBody,
    }: {
        requestBody: Product,
    }): CancelablePromise<any> {
        return __request(OpenAPI, {
            method: 'POST',
            url: '/products',
            body: requestBody,
            mediaType: 'application/json',
        });
    }

    /**
     * @returns any Success
     * @throws ApiError
     */
    public static getProducts1({
        id,
    }: {
        id: string,
    }): CancelablePromise<any> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/products/{id}',
            path: {
                'id': id,
            },
        });
    }

    /**
     * @returns any Success
     * @throws ApiError
     */
    public static putProducts({
        id,
        requestBody,
    }: {
        id: string,
        requestBody: Product,
    }): CancelablePromise<any> {
        return __request(OpenAPI, {
            method: 'PUT',
            url: '/products/{id}',
            path: {
                'id': id,
            },
            body: requestBody,
            mediaType: 'application/json',
        });
    }

    /**
     * @returns any Success
     * @throws ApiError
     */
    public static deleteProducts({
        id,
    }: {
        id: string,
    }): CancelablePromise<any> {
        return __request(OpenAPI, {
            method: 'DELETE',
            url: '/products/{id}',
            path: {
                'id': id,
            },
        });
    }

    /**
     * @returns string Success
     * @throws ApiError
     */
    public static get(): CancelablePromise<string> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/',
        });
    }

}