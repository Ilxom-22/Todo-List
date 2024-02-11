import type { AxiosError, AxiosInstance, AxiosResponse } from "axios";
import { ApiResponse } from "./ApiResponse";
import { ProblemDetails } from "./ProblemDetails";
import type { IMappable } from "@/infrastructure/mappers/IMappable";

export class ClientInterceptors {

    public static registerResponseConverter(client: AxiosInstance) {
        client.interceptors.response.use(<TResponse extends IMappable<TResponse>>(response: AxiosResponse<TResponse>) => {
                return {
                    ...response,
                    data: new ApiResponse(response.data as TResponse, null, response.status)
                }
            },
            (error: AxiosError) => {
                return {
                    ...error,
                    data: new ApiResponse(null, error.response?.data as ProblemDetails, error.response?.status ?? 500)
                };
            }
        );
    }
}
