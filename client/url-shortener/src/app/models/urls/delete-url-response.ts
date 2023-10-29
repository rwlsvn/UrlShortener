export interface DeleteUrlResponse {
    succeeded: boolean;
    errors: {
        code: string;
        description: string;
    }[];
}