import { describe, it, expect, vi } from 'vitest';
import '@testing-library/jest-dom';
import { render, screen } from '@testing-library/react';
import { Business } from 'src/api/BusinessClient';
import BusinessSearch from 'src/components/BusinessesSearch';
import * as BusinessHooks from 'src/hooks/BusinessHooks';

describe('BusinessSearch', () => {
  const useBusinessesSpy = vi.spyOn(BusinessHooks, 'useBusinesses');

  it('should render title', () => {
    useBusinessesSpy.mockReturnValue(
      new BusinessHooks.FetchBusinessesState('fetched', []),
    );
    render(<BusinessSearch />);
    const linkElement = screen.getByText(/Businesses/i);
    expect(linkElement).toBeInTheDocument();
  });

  it('should render business names', () => {
    const danFlashsName = "Dan Flash's";
    const truffonisName = "Truffoni's";
    useBusinessesSpy.mockReturnValue(
      new BusinessHooks.FetchBusinessesState('fetched', [
        new Business('123', danFlashsName),
        new Business('456', truffonisName),
      ]),
    );

    render(<BusinessSearch />);
    const dansElement = screen.getByText(danFlashsName);
    expect(dansElement).toBeInTheDocument();
    const truffonisElement = screen.getByText(truffonisName);
    expect(truffonisElement).toBeInTheDocument();
  });
});
