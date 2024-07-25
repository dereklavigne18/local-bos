import { describe, it, expect, vi } from 'vitest';
import '@testing-library/jest-dom';
import { render, screen } from '@testing-library/react';
import App from './App';
import * as BusinessHooks from 'src/hooks/BusinessHooks';

describe('App', () => {
  const useBusinessesSpy = vi.spyOn(BusinessHooks, 'useBusinesses');

  it('should render the title', () => {
    useBusinessesSpy.mockReturnValue(new BusinessHooks.FetchBusinessesState("fetched", []));
    render(<App />);
    const linkElement = screen.getByText(/Local Bos/i);
    expect(linkElement).toBeInTheDocument();
  })
});