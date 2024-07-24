import { describe, it, expect } from 'vitest';
import { render, screen } from '@testing-library/react';
import App from './App';

describe('App', () => {
  it('should render the title', () => {
    render(<App />);
    const linkElement = screen.getByText(/Local Bos/i);
    expect(linkElement).toBeInTheDocument();
  })
});