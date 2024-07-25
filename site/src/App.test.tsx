import { describe, it, expect, vi } from 'vitest';
import '@testing-library/jest-dom';
import { render, screen } from '@testing-library/react';
import App from './App';
import useBusinessesHooks, { FetchBusinessesState} from 'src/hooks/useBusinesses';

describe('App', () => {
  const useBusinessesSpy = vi.spyOn(useBusinessesHooks, 'useBusinesses');

  it('should render the title', () => {
    useBusinessesSpy.mockReturnValue(new FetchBusinessesState("fetched", []));
    render(<App />);
    const linkElement = screen.getByText(/Local Bos/i);
    expect(linkElement).toBeInTheDocument();
  })
});