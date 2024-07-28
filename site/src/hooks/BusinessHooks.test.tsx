import { describe, expect, it, vi } from 'vitest';
import { renderHook, waitFor } from '@testing-library/react';
import * as BusinessClient from 'src/api/BusinessClient';
import { useBusinesses } from 'src/hooks/BusinessHooks';

describe('BusinessClient', () => {
    const fetchBusinessesSpy = vi.spyOn(BusinessClient, 'fetchBusinesses');

    it('should return fetching status before call finishes', async () => {
        fetchBusinessesSpy.mockResolvedValue([]);
        const { result } = renderHook(() => useBusinesses());
        await waitFor(() => expect(result.current.status).toBe("fetching"));
    });

    it('should return fetched businesses', async () => {
        var danFlashs = new BusinessClient.Business("123", "Dan Flash's");
        var truffonis = new BusinessClient.Business("456", "Truffoni's");
        fetchBusinessesSpy.mockResolvedValue([danFlashs, truffonis]);

        const { result } = renderHook(() => useBusinesses());
        
        await waitFor(() => expect(result.current.status).toBe("fetched"), { timeout: 10000});
        
        expect(result.current.businesses).toStrictEqual([danFlashs, truffonis]);
    });
});