export interface AddUrlResponse {
    succeeded: boolean;
    id: string;
    errors: {
        code: string;
        description: string;
    }[];
}